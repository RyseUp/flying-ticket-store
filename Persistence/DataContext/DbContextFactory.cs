using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.DataContext
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {         
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Data Source=localhost,1435;Initial Catalog=LapGKBook;User ID=SA;Password=SqlServer@1234; TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
