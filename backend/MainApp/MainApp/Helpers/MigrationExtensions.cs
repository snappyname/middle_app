using DAL;
using Microsoft.EntityFrameworkCore;

namespace MainApp.Helpers;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        IServiceScope scope = app.ApplicationServices.CreateScope();
        AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}
