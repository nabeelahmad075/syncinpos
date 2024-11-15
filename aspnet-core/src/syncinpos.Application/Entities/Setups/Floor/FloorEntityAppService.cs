using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Setups.Floor.Dto;
using syncinpos.Entities.Setups.Floors;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Abp.UI;
using syncinpos.Entities.Setups.Tables.Dto;

namespace syncinpos.Entities.Setups.Floor
{
    public class FloorEntityAppService : AsyncCrudAppService<FloorEntity, FloorEntityDto>
    {
        public FloorEntityAppService(
            IRepository<FloorEntity, int> repository
            ) : base(repository) { }

        public async override Task<FloorEntityDto> CreateAsync(FloorEntityDto input)
        {
            await IsAlreadyTaken(input);
            var created = await base.CreateAsync(input);
            return created;
        }
        public async override Task<FloorEntityDto> UpdateAsync(FloorEntityDto input)
        {
            await IsAlreadyTaken(input);
            var updated = await base.UpdateAsync(input);
            return updated;
        }
        private async Task IsAlreadyTaken(FloorEntityDto input)
        {
            if (await IsAlreadyCreated(input.Title, input.LocationId, input.Id))
            {
                throw new UserFriendlyException($"Floor {input.Title}, already exists!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string FloorName, int? LocationId = null, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.Title == FloorName && a.LocationId == LocationId).AnyAsync();
        }

        public async Task<List<SelectItemDto>> GetFloorDropdownAsync(int? LocationId)
        {
            var floors = await Repository.GetAll()
                                         .Where(a => a.IsActive == true && a.LocationId == LocationId)
                                         .Select(a => new SelectItemDto
                                         {
                                             Label = a.Title,
                                             Value = a.Id
                                         }).ToListAsync();
            return floors;
        }

        public async Task<PagedResultDto<FloorHistoryDto>> GetFloorHistory(FloorHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                           .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                               a => a.Location.LocationName.Contains(input.Keyword) ||
                               a.Title.Contains(input.Keyword) ||
                               a.Remarks.Contains(input.Keyword)
                               )
                           .Select(x => new FloorHistoryDto
                           {
                               Id = x.Id,
                               LocationName = x.Location.LocationName,
                               Title = x.Title,
                               Remarks = x.Remarks,
                               IsActive = x.IsActive
                           });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<FloorHistoryDto>()
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
