using Abp.Application.Services.Dto;
using syncinpos.Entities.Accounts.DetailAccounts.Dto;
using syncinpos.Utility.SelectItemDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.DetailAccounts
{
    public interface IDetailAccountAppService
    {
        Task<DetailAccountDto> CreateAsync(DetailAccountDto input);
        Task<List<SelectItemDto>> GetDetailAccountDropdownAsync();
        Task<List<SelectItemDto>> GetDetailAccountDropdownOnAccountTypeAsync(string AccountType);
        Task<PagedResultDto<DetailAccountHistoryDto>> GetDetailAccountsHistory(DetailAccountHistoryPagedAndSortedResultRequestDto input);
        Task<string> GetDetailCodeById(int? detailAccountId = null);
        Task<string> GetNewDetailAccountCode(int? SubAccountId = null, int? id = null);
        Task<bool> IsAlreadyCreated(string DetailCode, int? id = null);
        Task<DetailAccountDto> UpdateAsync(DetailAccountDto input);
    }
}