using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BusinessLayer.Extensions
{
    /// <summary>
    /// Extension methods for configuring authentication services.
    /// </summary>
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// Adds custom JWT authentication to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to which the authentication services will be added.</param>
        /// <param name="configuration">The application configuration from which JWT settings will be retrieved.</param>
        /// <returns>The updated service collection.</returns>
        /// <remarks>
        /// This method configures JWT bearer token authentication using settings defined in the application configuration.
        /// </remarks>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
                };
            });

            return services;
        }
    }
}
