using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.IdentityData.DbContext;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceWb.Extensions
{
    public static class WebApplictionRegistration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
           await using var scope = app.Services.CreateAsyncScope();
            var dbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var pandingMigrations =await dbContextService.Database.GetPendingMigrationsAsync();
            if (pandingMigrations.Any())
               await dbContextService.Database.MigrateAsync();
            return app;
        }
        public static async Task<WebApplication> MigrateIdentityDatabaseAsync(this WebApplication app)
        {
           await using var scope = app.Services.CreateAsyncScope();
            var dbContextService = scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
            var pandingMigrations =await dbContextService.Database.GetPendingMigrationsAsync();
            if (pandingMigrations.Any())
               await dbContextService.Database.MigrateAsync();
            return app;
        }
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
           await using var scope = app.Services.CreateAsyncScope();
            var DataInitializerService = scope.ServiceProvider.GetRequiredKeyedService<IDataInitializer>("Default");
            await DataInitializerService.InitializeAsync();
            return app;

        }
        public static async Task<WebApplication> SeedIdentityDatabaseAsync(this WebApplication app)
        {
           await using var scope = app.Services.CreateAsyncScope();
            var DataInitializerService = scope.ServiceProvider.GetRequiredKeyedService<IDataInitializer>("Identity");
            await DataInitializerService.InitializeAsync();
            return app;

        }
    }

}
