using DomainLayer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DomainLayer.DBSeeding
{
    public static class DbInitializerExtension
    {
        public static IApplicationBuilder DbSeed(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<LeaveDbContext>();
            DbInitializer.Seed(context);
            return app;


        }
    }
}
