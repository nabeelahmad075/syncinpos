using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.HR.Departments.Dto;
using syncinpos.Entities.HR.Designations.Dto;
using syncinpos.Entities.Setups.Floor.Dto;
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
        public async override Task<DesignationsDto> CreateAsync(DesignationsDto input)
        {
            await IsAlreadyTaken(input);
            var created = await base.CreateAsync(input);
            return created;
        }
        public async override Task<DesignationsDto> UpdateAsync(DesignationsDto input)
        {
            await IsAlreadyTaken(input);
            var updated = await base.UpdateAsync(input);
            return updated;
        }
        private async Task IsAlreadyTaken(DesignationsDto input)
        {
            if (await IsAlreadyCreated(input.Title, input.Id))
            {
                throw new UserFriendlyException($"Designation {input.Title}, already exists!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string DesignationName, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.Title == DesignationName).AnyAsync();
        }
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
