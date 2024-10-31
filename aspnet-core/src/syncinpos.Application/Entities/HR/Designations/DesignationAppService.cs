using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.HR.Departments.Dto;
using syncinpos.Entities.HR.Designations.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Designations
{
    public class DesignationAppService : AsyncCrudAppService<Designation, DesignationsDto>
    {
        public DesignationAppService(
            IRepository<Designation, int> repository
            ) : base(repository) { }
        public async Task<List<SelectItemDto>> GetDesignationDropdown()
        {
            var designations = await Repository.GetAll()
                                                .Select(a => new SelectItemDto
                                                {
                                                    Label = a.Title,
                                                    Value = a.Id
                                                }).ToListAsync();
            return designations;
        }
        public async Task<PagedResultDto<DesignationHistoryDto>> GetHistoryAsync(DesignationHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                    a => a.Title.Contains(input.Keyword)
                )
                .Select(x => new DesignationHistoryDto
                {
                    Id = x.Id,
                    Title = x.Title,
                });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pageQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<DesignationHistoryDto>()
            {
                Items = await pageQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
