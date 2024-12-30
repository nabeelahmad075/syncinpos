using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Runtime;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Sessions
{
    public class SyncInPosSession : ClaimsAbpSession, ISyncInPosSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SyncInPosSession(
            IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider,
            IHttpContextAccessor httpContextAccessor)
            : base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Retrieve OpenedDay from session cookie
        public DateTime? OpenedDay
        {
            get
            {
                var cookieValue = _httpContextAccessor.HttpContext?.Request.Cookies["CurrDay"];
                if (DateTime.TryParse(cookieValue, out var OpenedDay))
                {
                    return OpenedDay;
                }
                return null;
            }
        }
    }
}
