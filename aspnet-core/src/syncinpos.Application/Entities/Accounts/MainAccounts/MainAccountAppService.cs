using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Accounts.MainAccounts.Dto;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using syncinpos.Entities.Accounts.Types;
using Abp.UI;

namespace syncinpos.Entities.Accounts.MainAccounts
{
    public class MainAccountAppService : AsyncCrudAppService<MainAccount, MainAccountDto>
    {
        private IRepository<MainType, int> mainTypeRepository;
        public MainAccountAppService(
            IRepository<MainAccount, int> repository,
            IRepository<MainType, int> _mainTypeRepository
            ) : base(repository) 
        {
            mainTypeRepository = _mainTypeRepository;
        }
        public async override Task<MainAccountDto> CreateAsync(MainAccountDto input)
        {
            await IsAlreadyTaken(input);
            return await base.CreateAsync(input);
        }
        public async override Task<MainAccountDto> UpdateAsync(MainAccountDto input)
        {
            await IsAlreadyTaken(input);
            return await base.UpdateAsync(input);
        }
        private async Task IsAlreadyTaken(MainAccountDto input)
        {
            if (await IsAlreadyCreated(input.MainCode, input.LocationId, input.Id))
            {
                throw new UserFriendlyException($"Main Code {input.MainCode}, already exists!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string MainCode, int? LocationId = null, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.MainCode == MainCode && a.LocationId == LocationId)
                                    .AnyAsync();
        }
        public async Task<string> GetNewMainAccountCode(int? LocationId = null)
        {
            string strDocNo = await Repository.GetAll()
                .Where(a => a.LocationId == LocationId)
                .OrderByDescending(b => b.MainCode)
                .Select(a => a.MainCode)
                .FirstOrDefaultAsync();

            int newDocNumber = 1;

            if (!string.IsNullOrEmpty(strDocNo))
            {
                int maxNumber = Convert.ToInt32(strDocNo);
                newDocNumber = maxNumber + 1;
            }

            string newMainCode = $"{newDocNumber:D2}";

            return newMainCode;
        }
        public async Task<List<SelectItemDto>> GetMainAccountDropdownAsync()
        {
            var mainAccounts = await Repository.GetAll()
                                               .Where(a => a.IsActive == true)
                                               .Select(a => new SelectItemDto
                                               {
                                                   Label = $"{a.MainCode} - {a.MainTitle}",
                                                   Value = a.Id
                                               }).ToListAsync();
            return mainAccounts;
        }
        public async Task<List<SelectItemDto>> GetMainTypeDropdownAsync()
        {
            var mainTypes = await mainTypeRepository.GetAll()
                                                    .Where(a => a.IsActive == true)
                                                    .Select(a => new SelectItemDto
                                                    {
                                                        Label = a.Title,
                                                        Value = a.Id
                                                    }).ToListAsync();
            return mainTypes;
        }
        public async Task<PagedResultDto<MainAccountHistoryDto>> GetMainAccountHistoryAsync(MainAccountHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input).
                           WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                           a => a.MainCode.Contains(input.Keyword) ||
                           a.MainType.Title.Contains(input.Keyword) ||
                           a.MainTitle.Contains(input.Keyword) ||
                           a.MainCode.Contains(input.Keyword)
                           ).Select(x => new MainAccountHistoryDto
                           {
                               Id = x.Id,
                               MainCode = x.MainCode,
                               MainTitle = x.MainTitle,
                               MainType = x.MainType.Title,
                               IsActive = x.IsActive
                           });

            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<MainAccountHistoryDto>
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
