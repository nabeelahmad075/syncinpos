using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using syncinpos.Authorization.Roles;
using syncinpos.Authorization.Users;
using syncinpos.MultiTenancy;
using syncinpos.Entities.LocationTypes;
using syncinpos.Entities.Regions;
using syncinpos.Entities.Locations;
using syncinpos.Entities.HR.Departments;
using syncinpos.Entities.HR.Designations;

namespace syncinpos.EntityFrameworkCore
{
    public class syncinposDbContext : AbpZeroDbContext<Tenant, Role, User, syncinposDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public syncinposDbContext(DbContextOptions<syncinposDbContext> options)
            : base(options)
        {
        }
        public DbSet<LocationType> tblLocationTypes { get; set; }
        public DbSet<Region> tblRegions { get; set; }
        public DbSet<Location> tblLocations { get; set; }
        public DbSet<Department> tblDepartments { get; set; }
        public DbSet<Designation> tblDesignations { get; set; }
    }
}
