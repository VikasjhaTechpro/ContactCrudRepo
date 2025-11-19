using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContactApp.Infrastructure.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Use your SQL Server connection string here
            optionsBuilder.UseSqlServer(
                "Server=VIKASH\\VIKASH;Database=ContactDb;Trusted_Connection=True;Encrypt=False;"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
