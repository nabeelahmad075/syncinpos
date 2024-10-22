using System.Threading.Tasks;
using syncinpos.Configuration.Dto;

namespace syncinpos.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
