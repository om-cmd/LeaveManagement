using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;

namespace BusinessLayer.SwaggerService
{
    public static class Swagger
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
