using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.HR.Departments.Dto;
using syncinpos.Entities.Locations.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.HR.Departments
{
    public class DepartmentAppService : AsyncCrudAppService<Department, DepartmentsDto>
    {
        public DepartmentAppService(
            IRepository<Department, int> repository
            ) : base(repository) { }
        public async Task<List<SelectItemDto>> GetDepartmentsDropDown()
        {
            var departments = await Repository.GetAll()
                                              .Select(a => new SelectItemDto
                                              {
                                                  Label = a.Title,
                                                  Value = a.Id
                                              }).ToListAsync();
            return departments;
        }
        public async Task<PagedResultDto<DepartmentsHistoryDto>> GetHistoryAsync(DepartmentsHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                    a => a.Title.Contains(input.Keyword)
                )
                .Select(x => new DepartmentsHistoryDto
                {
                    Id = x.Id,
                    Title = x.Title,
                });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pageQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<DepartmentsHistoryDto>()
            {
                Items = await pageQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
