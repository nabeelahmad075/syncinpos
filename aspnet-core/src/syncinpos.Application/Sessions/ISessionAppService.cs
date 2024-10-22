using System.Threading.Tasks;
using Abp.Application.Services;
using syncinpos.Sessions.Dto;

namespace syncinpos.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
