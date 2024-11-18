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
using syncinpos.Entities.HR.Employees;
using System.Security.AccessControl;
using syncinpos.Entities.Inventory.Sections;
using syncinpos.Entities.Inventory.ItemTypes;
using syncinpos.Entities.Inventory.ItemCategories;
using syncinpos.Entities.Inventory.Units;
using syncinpos.Entities.Inventory.Items;
using syncinpos.Entities.Setups.Floors;
using syncinpos.Entities.Setups.Tables;
using syncinpos.Entities.Accounts.Types;
using syncinpos.Entities.Accounts.MainAccounts;
using syncinpos.Entities.Accounts.SubAccounts;

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
        public DbSet<Employee> tblEmployees { get; set; }
        public DbSet<Section> tblSections { get; set; }
        public DbSet<ItemType> tblItemTypes { get; set; }
        public DbSet<ItemCategory> tblItemCategories { get; set; }
        public DbSet<UnitOfMeasurement> tblUnitOfMeasurements { get; set; }
        public DbSet<Item> tblItemDefinitions { get; set; }
        public DbSet<FloorEntity> tblFloors { get; set; }
        public DbSet<TableEntity> tblTables { get; set; }
        public DbSet<MainType> tblMainTypes { get; set; }
        public DbSet<AccountType> tblAccountTypes { get; set; }
        public DbSet<MainAccount> tblMainAccounts { get; set; }
        public DbSet<SubAccount> tblSubAccounts { get; set; }
    }
}
