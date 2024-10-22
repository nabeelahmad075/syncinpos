using System.Threading.Tasks;
using Abp.Application.Services;
using syncinpos.Authorization.Accounts.Dto;

namespace syncinpos.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
