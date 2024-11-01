using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Inventory.Sections.Dto;
using syncinpos.Entities.Locations.Dto;
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
    }
}
