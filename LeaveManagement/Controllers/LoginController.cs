using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.ViewModels;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LeaveManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LoginController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel login, string username, string password)
        {
            var authenticatedUser = _unitOfWork.Context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var mapper = _mapper.Map<User>(login);

                // Authenticate user check hanna tyo suru ko unitofwork bata taneko kura haru
                if (authenticatedUser == null)
                {
                    // Authentication failed
                    return BadRequest("Invalid email or password");
                }

                // Authentication successful vaye paxi hai , create claims identity token jastei ho jwt le jastei yo chai k chainxa tyo store garxa 
                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, authenticatedUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier, authenticatedUser.UserID.ToString())
                    // Add more claims as needed
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in the user
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Ok(new { message = "Login successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred during login", message = ex.Message });
            }
        }

    }
}

