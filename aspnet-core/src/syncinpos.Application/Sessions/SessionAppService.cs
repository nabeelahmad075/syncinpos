using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;
using syncinpos.Entities.Sales.DayCloses;
using syncinpos.Sessions.Dto;

namespace syncinpos.Sessions
{
    public class SessionAppService : syncinposAppServiceBase, ISessionAppService
    {
        private readonly DayCloseAppService _dayCloseAppService;
        public SessionAppService(
            DayCloseAppService dayCloseAppService
            ) 
        {
            _dayCloseAppService = dayCloseAppService;
        }
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                output.Application.OpenedDay = await _dayCloseAppService.GetOpenedDay();
            }

            return output;
        }
    }
}
