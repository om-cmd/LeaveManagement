using DomainLayer.Interface.IService;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{

    [Authorize(Roles ="SuperAdmin,Admin")]
    
    
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _services;
        public RegisterController(IRegisterService registered)
        {
           _services = registered;
        }
        [HttpPost("Register")]
        public IActionResult Register(RegisterViewModel registerModel)
        {
            var register = _services.Register(registerModel);
            return Ok(register);
        }

    }

}