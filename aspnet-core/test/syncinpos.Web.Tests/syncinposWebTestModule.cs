using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using syncinpos.EntityFrameworkCore;
using syncinpos.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace syncinpos.Web.Tests
{
    [DependsOn(
        typeof(syncinposWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class syncinposWebTestModule : AbpModule
    {
        public syncinposWebTestModule(syncinposEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(syncinposWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(syncinposWebMvcModule).Assembly);
        }
    }
}