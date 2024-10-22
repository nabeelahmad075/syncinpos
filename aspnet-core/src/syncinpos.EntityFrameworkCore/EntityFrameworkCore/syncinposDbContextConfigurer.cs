using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace syncinpos.EntityFrameworkCore
{
    public static class syncinposDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<syncinposDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<syncinposDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
