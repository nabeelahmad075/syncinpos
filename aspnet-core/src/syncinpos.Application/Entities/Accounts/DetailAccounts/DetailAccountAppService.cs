using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using syncinpos.Entities.Accounts.DetailAccounts.Dto;
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
        public DetailAccountAppService(
            IRepository<DetailAccount, int> repository
            ) : base(repository) { }

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
