using DomainLayer.Interface.IService;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    /// <summary>
    /// Provides methods for user authentication and token generation.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        /// <summary>
        /// use for login Service
        /// </summary>
        /// <param name="loginService"></param>
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Authenticates a user and generates JWT tokens.
        /// </summary>
        /// <param name="login">The login view model containing username and password.</param>
        /// <returns>The generated JWT tokens.</returns>
        /// <exception cref="Exception">Thrown when the username and password do not match.</exception>

        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Login(LoginViewModel login)
        {
            var log = _loginService.Login(login);
            return Ok(log);
        }
    }
}
