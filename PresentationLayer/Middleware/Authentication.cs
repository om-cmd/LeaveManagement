using LeaveManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Middleware
{
    public class Authentication
    {
        private readonly IConfiguration _configuration;

        public Authentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string AccessToken, string RefreshToken) ProvideBothToken(Person person)
        {
            var accessToken = GenerateJwtToken(person);
            var refreshToken = GenerateRefreshToken();
            return (accessToken, refreshToken);
        }

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
