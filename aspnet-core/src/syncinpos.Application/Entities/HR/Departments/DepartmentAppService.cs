using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.HR.Departments.Dto;
using syncinpos.Entities.Locations.Dto;
using syncinpos.Entities.Setups.Floor.Dto;
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
        public async override Task<DepartmentsDto> CreateAsync(DepartmentsDto input)
        {
            await IsAlreadyTaken(input);
            var created = await base.CreateAsync(input);
            return created;
        }
        public async override Task<DepartmentsDto> UpdateAsync(DepartmentsDto input)
        {
            await IsAlreadyTaken(input);
            var updated = await base.UpdateAsync(input);
            return updated;
        }
        private async Task IsAlreadyTaken(DepartmentsDto input)
        {
            if (await IsAlreadyCreated(input.Title, input.Id))
            {
                throw new UserFriendlyException($"Department {input.Title}, already exists!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string DepartmentName, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.Title == DepartmentName).AnyAsync();
        }
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
