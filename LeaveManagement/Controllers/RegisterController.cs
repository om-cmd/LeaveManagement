using DomainLayer.Interface.IService;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    /// <summary>
    /// Provides methods for user registration.
    /// </summary>
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _services;
        /// <summary>
        /// Used to get the register serivce
        /// </summary>
        /// <param name="registered">use to call the service</param>
        public RegisterController(IRegisterService registered)
        {
            _services = registered;
        }


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="register">The register view model containing user details.</param>
        /// <returns>The registered employee entity.</returns>
        /// <exception cref="Exception">Thrown when the user already exists or the CreatedBy field is null or empty.</exception>

        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Register(RegisterViewModel register)
        {
            var registers = _services.Register(register);
            return Ok(registers);
        }
    }
}
