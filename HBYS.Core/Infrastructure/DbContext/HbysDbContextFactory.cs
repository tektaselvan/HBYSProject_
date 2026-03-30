using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HBYS.Core.Infrastructure.DbContext;

public class HbysDbContextFactory : IDesignTimeDbContextFactory<HbysDbContext>
{
    public HbysDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<HbysDbContext>();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        return new HbysDbContext(optionsBuilder.Options);
    }
}
