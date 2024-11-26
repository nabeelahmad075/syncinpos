using Abp.Application.Services.Dto;
using syncinpos.Entities.Accounts.SubAccounts.Dto;
using syncinpos.Utility.SelectItemDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.SubAccounts
{
    public interface ISubAccountAppService
    {
        Task<SubAccountDto> CreateAsync(SubAccountDto input);
        Task<List<SelectItemDto>> GetAccountTypeDropdownAsync();
        Task<string> GetNewSubAccountCodeAsync(int? MainAccountId);
        Task<List<SelectItemDto>> GetSubAccountDropdownAsync();
        Task<PagedResultDto<SubAccountHistoryDto>> GetSubAccountHistoryAsync(SubAccountHistoryPagedAndSortedResultRequestDto input, int? AccountTypeId = null);
        Task<bool> IsAlreadyCreated(string SubCode, int? id = null);
        Task IsAlreadyTaken(SubAccountDto input);
        Task<bool> IsControlAccount(int? SubAccountId = null);
        Task<SubAccountDto> UpdateAsync(SubAccountDto input);
    }
}