using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Inventory.Sections.Dto;
using syncinpos.Entities.Locations.Dto;
using syncinpos.Entities.Setups.Floor.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Sections
{
    public class SectionsAppService : AsyncCrudAppService<Section, SectionDto>
    {
        public SectionsAppService(
            IRepository<Section, int> repository
            ) : base(repository) { }
        public async override Task<SectionDto> CreateAsync(SectionDto input)
        {
            await IsAlreadyTaken(input);
            var created = await base.CreateAsync(input);
            return created;
        }
        public async override Task<SectionDto> UpdateAsync(SectionDto input)
        {
            await IsAlreadyTaken(input);
            var updated = await base.UpdateAsync(input);
            return updated;
        }
        private async Task IsAlreadyTaken(SectionDto input)
        {
            if (await IsAlreadyCreated(input.Title, input.Id))
            {
                throw new UserFriendlyException($"Section {input.Title}, already exists!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string SectionName, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.Title == SectionName).AnyAsync();
        }
        public async Task<PagedResultDto<SectionHistoryDto>> GetSectionsHistoryAsync(SectionHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                    a => a.Title.Contains(input.Keyword)
                )
                .Select(x => new SectionHistoryDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    IsActive = x.IsActive
                });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pageQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<SectionHistoryDto>()
            {
                Items = await pageQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
        public async Task<List<SelectItemDto>> GetSectionsDropdownAsync(int? ItemId, int? SectionId)
        {
            var Sections = await Repository.GetAll()
                                      .Where(a => (a.IsActive == true && ItemId == 0) || ((a.IsActive == true && ItemId != 0) || a.Id == SectionId))
                                      .Select(a => new SelectItemDto
                                      {
                                          Label = a.Title,
                                          Value = a.Id
                                      }).ToListAsync();
            return Sections;
        }
    }
}
