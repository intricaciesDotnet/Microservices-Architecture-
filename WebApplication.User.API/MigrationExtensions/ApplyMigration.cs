using Microsoft.EntityFrameworkCore;
using WebApplication.User.API.ApplicationDb;

namespace WebApplication.User.API.MigrationExtensions;

public static class ApplyMigration
{
    public static void ApplyMigrate(this IApplicationBuilder applicationBuilder)
    {
        using IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope();

        using AppDbContext appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

        appDbContext.Database.Migrate();
    }
}
