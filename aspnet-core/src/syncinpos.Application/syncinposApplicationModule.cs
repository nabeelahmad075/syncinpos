using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using syncinpos.Authorization;

namespace syncinpos
{
    [DependsOn(
        typeof(syncinposCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class syncinposApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<syncinposAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(syncinposApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
