using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using syncinpos.Configuration.Dto;

namespace syncinpos.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : syncinposAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
