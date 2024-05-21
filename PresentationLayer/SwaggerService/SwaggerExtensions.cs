using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BusinessLayer.SwaggerService
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
             {
                 options.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });

                 options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Name = "Authorization",
                     Type = SecuritySchemeType.Http,
                     Scheme = "Bearer",
                     BearerFormat = "JWT",
                     In = ParameterLocation.Header,
                     Description = "JWT Authorization header using the Bearer scheme."
                 });

                 options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                 });
             });

            return services;
        }

    }
}

