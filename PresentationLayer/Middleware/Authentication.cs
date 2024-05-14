    using LeaveManagement.Models;
    using Microsoft.AspNetCore.Mvc.Razor;
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
            public (string AcessToken, string RefreshToken) ProvideBothToken(User user)
            {
                var acessToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();
                return (acessToken, refreshToken);
            }

            public string GenerateJwtToken(User user)
            {
                var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));  
                var credintials = new SigningCredentials(SecurityKey,SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName) 
                };
                var token = new JwtSecurityToken
                    (
                    _configuration["jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:AcessTokenExpiresInMinutes"])),
                    signingCredentials: credintials);
                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            private string GenerateRefreshToken()
            {
                var refreshTokenTime = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiresInDays"]));
                var randomNumber = new byte[32];
                using(var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    return Convert.ToBase64String(randomNumber);    
                }
            }
         
        }
    }
