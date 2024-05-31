using LeaveManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Middleware
{
    /// <summary>
    /// Provides methods for authentication and token generation.
    /// </summary>
    public class Authentication
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentication"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Authentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates both access and refresh tokens for the provided person.
        /// </summary>
        /// <param name="person">The person for whom tokens are generated.</param>
        /// <returns>A tuple containing the access token and refresh token.</returns>
        public (string AccessToken, string RefreshToken) ProvideBothToken(Person person)
        {
            var accessToken = GenerateJwtToken(person);
            var refreshToken = GenerateRefreshToken();
            return (accessToken, refreshToken);
        }

        /// <summary>
        /// Generates a JWT access token for the provided person.
        /// </summary>
        /// <param name="person">The person for whom the token is generated.</param>
        /// <returns>The generated JWT access token.</returns>
        public string GenerateJwtToken(Person person)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, person.Id.ToString()),
                new Claim(ClaimTypes.Name, person.UserName),
                new Claim(ClaimTypes.Role, person.Roles.ToString())
                // Add additional claims for other properties of Person if needed
            };

            if (!int.TryParse(_configuration["Jwt:AccessTokenExpiresInMinutes"], out int accessTokenExpiresInMinutes))
            {
                throw new ArgumentException("Invalid access token expiration time in configuration.");
            }

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(accessTokenExpiresInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generates a random refresh token.
        /// </summary>
        /// <returns>The generated refresh token.</returns>
        private string GenerateRefreshToken()
        {
            if (!int.TryParse(_configuration["Jwt:RefreshTokenExpiresInDays"], out int refreshTokenExpiresInDays))
            {
                throw new ArgumentException("Invalid refresh token expiration time in configuration.");
            }

            var refreshTokenTime = DateTime.Now.AddDays(refreshTokenExpiresInDays);
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
