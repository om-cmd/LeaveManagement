using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace BusinessLayer.SwaggerService
{
    /// <summary>
    /// Extension methods for configuring Swagger middleware.
    /// </summary>
    public static class Swagger
    {
        /// <summary>
        /// Adds Swagger middleware to the specified <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="app">The application builder to which Swagger middleware will be added.</param>
        /// <returns>The updated application builder.</returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
