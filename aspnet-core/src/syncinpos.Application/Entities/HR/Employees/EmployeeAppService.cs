using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using syncinpos.Entities.HR.Employees.Dto;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace syncinpos.Entities.HR.Employees
{
    public class EmployeeAppService : AsyncCrudAppService<Employee, EmployeeDto>
    {
        public EmployeeAppService(
            IRepository<Employee, int> repository
            ) : base(repository) 
        { 
        
        }
        public async Task<PagedResultDto<EmployeeHistoryDto>> GetEmployeesHistory(EmployeeHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                            .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                            a => a.Location.LocationName.Contains(input.Keyword) || a.EmployeeName.Contains(input.Keyword)
                            ).Select(x => new EmployeeHistoryDto
                            {
                                Id = x.Id,
                                LocationName = x.Location.LocationName,
                                EmployeeName = x.EmployeeName,
                                Designation = x.Designation.Title,
                                Department = x.Department.Title,
                                MobileNo = x.MobileNo,
                                Address = x.Address,
                                IsActive = x.IsActive,
                                CreationTime = x.CreationTime,
                                LastModificationTime = x.LastModificationTime,
                                JoiningDate = x.JoiningDate
                            });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pageQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<EmployeeHistoryDto>()
            {
                Items = await pageQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
