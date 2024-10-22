using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using syncinpos.Authorization.Roles;
using syncinpos.Authorization.Users;
using syncinpos.MultiTenancy;

namespace syncinpos.EntityFrameworkCore
{
    public class syncinposDbContext : AbpZeroDbContext<Tenant, Role, User, syncinposDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public syncinposDbContext(DbContextOptions<syncinposDbContext> options)
            : base(options)
        {
        }
    }
}
