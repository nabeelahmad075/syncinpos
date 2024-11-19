using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Accounts.DetailAccounts.Dto;
using syncinpos.Entities.Accounts.MainAccounts;
using syncinpos.Entities.Accounts.MainAccounts.Dto;
using syncinpos.Entities.Accounts.SubAccounts;
using syncinpos.Utility.SelectItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.DetailAccounts
{
    public class DetailAccountAppService : AsyncCrudAppService<DetailAccount, DetailAccountDto>
    {
        private IRepository<SubAccount, int> subAccountRepository;
        public DetailAccountAppService(
            IRepository<DetailAccount, int> repository,
            IRepository<SubAccount, int> _subAccountRepository
            ) : base(repository) 
        {
            subAccountRepository = _subAccountRepository;
        }
        public async override Task<DetailAccountDto> CreateAsync(DetailAccountDto input)
        {
            await IsAlreadyTaken(input);
            return await base.CreateAsync(input);
        }
        public async override Task<DetailAccountDto> UpdateAsync(DetailAccountDto input)
        {
            await IsAlreadyTaken(input);
            return await base.UpdateAsync(input);
        }
        private async Task IsAlreadyTaken(DetailAccountDto input)
        {
            if (await IsAlreadyCreated(input.DetailCode, input.Id))
            {
                throw new UserFriendlyException($"Detail Code {input.DetailCode}, already exists!");
            }
        }
        public async Task<bool> IsAlreadyCreated(string DetailCode, int? LocationId = null, int? id = null)
        {
            return await Repository.GetAll()
                                    .WhereIf(id != null && id > 0, a => a.Id != id)
                                    .Where(a => a.DetailCode == DetailCode)
                                    .AnyAsync();
        }
        public async Task<string> GetNewDetailAccountCode(int? SubAccountId = null)
        {
            string strDocNo = await Repository.GetAll()
                .Where(a => a.SubAccountId == SubAccountId)
                .OrderByDescending(b => b.DetailCode)
                .Select(a => a.DetailCode)
                .FirstOrDefaultAsync();

            var strSubCode = await subAccountRepository.GetAll()
                                             .Where(a => a.Id == SubAccountId)
                                             .Select(a => a.SubCode)
                                             .FirstOrDefaultAsync();

            int newDocNumber = 1;

            if (!string.IsNullOrEmpty(strDocNo))
            {
                int maxNumber = Convert.ToInt32(strDocNo.Substring(6));
                newDocNumber = maxNumber + 1;
            }

            string newDetailCode = $"{strSubCode}-{newDocNumber:D4}";

            return newDetailCode;
        }
        public async Task<List<SelectItemDto>> GetDetailAccountDropdownAsync()
        {
            var detailAccounts = await Repository.GetAll()
                                                 .Where(a => a.IsActive == true)
                                                 .Select(a => new SelectItemDto
                                                 {
                                                     Label = a.DetailTitle,
                                                     Value = a.Id
                                                 }).ToListAsync();
            return detailAccounts;
        }
        public async Task<PagedResultDto<DetailAccountHistoryDto>> GetDetailAccountsHistory(DetailAccountHistoryPagedAndSortedResultRequestDto input)
        {
            var sqlQuery = CreateFilteredQuery(input)
                                      .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword),
                                      a => a.DetailCode.Contains(input.Keyword) ||
                                      a.DetailTitle.Contains(input.Keyword) ||
                                      a.SubAccount.AccountType.Title.Contains(input.Keyword) ||
                                      a.SubAccount.SubTitle.Contains(input.Keyword) ||
                                      a.SubAccount.MainAccount.MainTitle.Contains(input.Keyword)
                                      ).Select(x => new DetailAccountHistoryDto
                                      {
                                          Id = x.Id,
                                          DetailCode = x.DetailCode,
                                          DetailTitle = x.DetailTitle,
                                          AccountType = x.SubAccount.AccountType.Title,
                                          SubAccount = x.SubAccount.SubTitle,
                                          MainAccount = x.SubAccount.MainAccount.MainTitle,
                                          IsActive = x.IsActive
                                      });
            var sortedQuery = sqlQuery.OrderBy(x => input.Sorting);
            var pagedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<DetailAccountHistoryDto>
            {
                Items = await pagedQuery.ToListAsync(),
                TotalCount = sqlQuery.Count()
            };
        }
    }
}
