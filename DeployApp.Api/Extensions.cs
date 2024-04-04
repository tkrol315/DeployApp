using DeployApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DeployApp.Api
{
    public static class Extensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<DeployAppDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
