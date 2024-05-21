using DomainLayer.Interface.IService;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
      private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            var log = _loginService.Login(login);
            return Ok(log);
        }


    }
}

