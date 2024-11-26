using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Accounts.MainAccounts;
using syncinpos.Entities.Accounts.SubAccounts.Dto;
using syncinpos.Entities.Accounts.Types;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.SubAccounts
{
    public class SubAccountAppService : AsyncCrudAppService<SubAccount, SubAccountDto>, ISubAccountAppService
    {
        private IRepository<AccountType, int> accountTypeRepository;
        private IRepository<MainAccount, int> mainAccountRepository;
        public SubAccountAppService(
            IRepository<SubAccount, int> repository,
            IRepository<AccountType, int> _accountTypeRepository,
            IRepository<MainAccount, int> _mainAccountRepository
            ) : base(repository)
        {
            accountTypeRepository = _accountTypeRepository;
            mainAccountRepository = _mainAccountRepository;
        }
        public async override Task<SubAccountDto> CreateAsync(SubAccountDto input)
        {
            await IsAlreadyTaken(input);
            return await base.CreateAsync(input);
        }
        public async override Task<SubAccountDto> UpdateAsync(SubAccountDto input)
        {
            await IsAlreadyTaken(input);
            return await base.UpdateAsync(input);
        }
        public async Task IsAlreadyTaken(SubAccountDto input)
        {
            if (await IsAlreadyCreated(input.SubCode, input.Id))
            {
                throw new UserFriendlyException($"Sub code {input.SubCode}, already exists.");
            }
        }
        public async Task<bool> IsAlreadyCreated(string SubCode, int? id = null)
        {
            return await Repository.GetAll()
                                   .WhereIf(id != null && id > 0, a => a.Id != id)
                                   .Where(a => a.SubCode == SubCode)
                                   .AnyAsync();
        }
        public async Task<bool> IsControlAccount(int? SubAccountId = null)
        {
            return await Repository.GetAll()
                                   .Where(a => a.Id == SubAccountId && a.IsControlAccount == true)
                                   .AnyAsync();
        }
        public async Task<string> GetNewSubAccountCodeAsync(int? MainAccountId, int? id = null)
        {
            var strSubCode = await Repository.GetAll()
                                             .Where(a => a.MainAccountId == MainAccountId)
                                             .OrderByDescending(a => a.SubCode)
                                             .Select(a => a.SubCode)
                                             .FirstOrDefaultAsync();

            var mainAccountNotChanged = await Repository.GetAll()
                                                    .WhereIf(id.HasValue && id > 0, a => a.Id == id)
                                                    .AnyAsync(a => a.MainAccountId == MainAccountId);

            if (!mainAccountNotChanged)
            {
                return strSubCode;
            }

            var strMainCode = await mainAccountRepository.GetAll()
                                             .Where(a => a.Id == MainAccountId)
                                             .Select(a => a.MainCode)
                                             .FirstOrDefaultAsync();

            int NewDocNumber = 1;
            if (!string.IsNullOrEmpty(strSubCode))
            {
                int NextNumber = Convert.ToInt32(strSubCode.Substring(3));
                NewDocNumber = NextNumber + 1;
            }

            string newSubCode = $"{strMainCode}-{NewDocNumber:D3}";
            return newSubCode;
        }
        public async Task<List<SelectItemDto>> GetSubAccountDropdownAsync()
        {
            var subAccounts = await Repository.GetAll()
                                              .Where(a => a.IsActive == true)
                                              .Select(a => new SelectItemDto
                                              {
                                                  Label = $"{a.SubCode} - {a.SubTitle}",
                                                  Value = a.Id
                                              }).ToListAsync();
            return subAccounts;
        }
        public async Task<List<SelectItemDto>> GetSubAccountDropdownOnAccountTypeAsync(string AccountType)
        {
            var subAccounts = await Repository.GetAll()
                                              .Where(a => a.IsActive == true && a.AccountType.Title == AccountType)
                                              .Select(a => new SelectItemDto
                                              {
                                                  Label = $"{a.SubCode} - {a.SubTitle}",
                                                  Value = a.Id
                                              }).ToListAsync();
            return subAccounts;
        }
        public async Task<List<SelectItemDto>> GetAccountTypeDropdownAsync()
        {
            var accountTypes = await accountTypeRepository.GetAll()
                                                         .Select(a => new SelectItemDto
                                                         {
                                                             Label = a.Title,
                                                             Value = a.Id,
                                                             Code = a.IsControlAccount ? "True" : "False"
                                                         }).ToListAsync();
            return accountTypes;
        }
        public async Task<PagedResultDto<SubAccountHistoryDto>> GetSubAccountHistoryAsync(SubAccountHistoryPagedAndSortedResultRequestDto input, int? AccountTypeId = null)
        {
            var sqlQuery = CreateFilteredQuery(input)
                .WhereIf(AccountTypeId.HasValue, a => a.AccountTypeId == AccountTypeId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                a => a.SubCode.Contains(input.Keyword) ||
                a.SubTitle.Contains(input.Keyword) ||
                a.AccountType.Title.Contains(input.Keyword) ||
                a.MainAccount.MainTitle.Contains(input.Keyword)
                ).Select(x => new SubAccountHistoryDto
                {
                    Id = x.Id,
                    SubCode = x.SubCode,
                    SubTitle = x.SubTitle,
                    AccountType = x.AccountType.Title,
                    MainAccountTitle = x.MainAccount.MainTitle,
                    IsActive = x.IsActive,
                    IsControlAccount = x.IsControlAccount
                });

            var sortedQuery = sqlQuery.OrderBy(a => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<SubAccountHistoryDto>
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
