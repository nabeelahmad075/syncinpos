using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Locations.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Locations
{
    public class LocationAppService : AsyncCrudAppService<Location, LocationDto>
    {
        public LocationAppService(
            IRepository<Location, int> repository
            ) : base(repository) { }
        public async override Task<LocationDto> CreateAsync(LocationDto input)
        {
            await IsCodeAlreadyTaken(input);
            var created = await base.CreateAsync(input);
            return created;
        }
        private async Task IsCodeAlreadyTaken(LocationDto input)
        {
            if (await IsAlreadyCreated(input.LocationCode, input.Id))
            {
                throw new UserFriendlyException($"Location Code {input.LocationCode}, already taken!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string code, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.LocationCode == code).AnyAsync();
        }
        public override async Task<LocationDto> UpdateAsync(LocationDto input)
        {
            await IsCodeAlreadyTaken(input);
            var updated = await base.UpdateAsync(input);
            return updated;
        }
        public async Task<PagedResultDto<LocationHistoryDto>> GetHistoryAsync(LocationHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                    a => a.LocationCode.Contains(input.Keyword) || a.LocationName.Contains(input.Keyword)
                )
                .Select(x => new LocationHistoryDto
                {
                    Id = x.Id,
                    RegionName = x.Region.Title,
                    LocationName = x.LocationName,
                    LocationType = x.LocationType.Title,
                    Address = x.Address,
                    ContactNumber = x.ContactNumber,
                    ContactPerson = x.ContactPerson,
                    IsActive = x.IsActive,
                    CreationTime = x.CreationTime,
                    LastModificationTime = x.LastModificationTime
                });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pageQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<LocationHistoryDto>()
            {
                Items = await pageQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
        public async Task<List<SelectItemDto>> GetLocationDropDown()
        {
            var locations = await Repository.GetAll()
                                            .Select(a => new SelectItemDto
                                            {
                                                Label = a.LocationName,
                                                Value = a.Id
                                            }).ToListAsync();
            return locations;
        }
    }
}
