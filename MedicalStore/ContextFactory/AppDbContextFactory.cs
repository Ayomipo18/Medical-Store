using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository.DbContext;

namespace MedicalStore.ContextFactory
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(config.GetConnectionString("sqlConnection"),
                b => b.MigrationsAssembly("MedicalStore"));
            return new AppDbContext(builder.Options);
        }
    }
}
