using DomainLayer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DomainLayer.DBSeeding
{
    public static class DbInitializerExtension
    {
        /// <summary>
        /// Extension method to seed the database.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        /// <returns>The application builder instance.</returns>
        public static IApplicationBuilder DbSeed(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<LeaveDbContext>();
            DbInitializer.Seed(context);
            return app;
        }
    }
}
