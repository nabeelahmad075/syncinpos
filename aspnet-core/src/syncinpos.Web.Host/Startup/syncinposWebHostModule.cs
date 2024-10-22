using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using syncinpos.Configuration;

namespace syncinpos.Web.Host.Startup
{
    [DependsOn(
       typeof(syncinposWebCoreModule))]
    public class syncinposWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public syncinposWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(syncinposWebHostModule).GetAssembly());
        }
    }
}
