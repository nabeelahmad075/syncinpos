using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace syncinpos.Controllers
{
    public abstract class syncinposControllerBase: AbpController
    {
        protected syncinposControllerBase()
        {
            LocalizationSourceName = syncinposConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
