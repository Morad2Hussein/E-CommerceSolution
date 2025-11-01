using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceWb.Extensions
{
    public  static class WebApplictionRegistration
    {
        public static WebApplication MigrateDatabase (this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            if (dbContextService.Database.GetPendingMigrations().Any())
                dbContextService.Database.Migrate();
            return app;
        }
        public static WebApplication SeedDatabase (this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var DataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            DataInitializerService.Initialize();
            return app;

        }
    }
           
}
