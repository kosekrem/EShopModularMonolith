using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigrations<TContext>(this IApplicationBuilder app)
        where TContext : DbContext
    {
        MigrateDataBaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();
        
        SeedDataAsync(app.ApplicationServices).GetAwaiter().GetResult();
        
        return app;
    }

    private static async Task MigrateDataBaseAsync<TContext>(IServiceProvider serviceProvider) 
        where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        
        await context.Database.MigrateAsync();
    }

    private static async Task SeedDataAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAllAsync();
        }
    }
}