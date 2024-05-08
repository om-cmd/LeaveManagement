using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.ViewModels;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public IActionResult Register(RegisterViewModel registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingUser = _unitOfWork.Context.Users.FirstOrDefault(u => u.Email == registerModel.Email);
                if (existingUser != null)
                {
                    return Conflict("User with this email already exists");
                }

                var user = _mapper.Map<User>(registerModel);

                // Additional logic for user creation, such as password hashing, 

                _unitOfWork.Context.Users.Add(user);
                _unitOfWork.Context.SaveChanges();

                return Ok(new { message = "Registration successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred during registration", message = ex.Message });
            }
        }

    }

}