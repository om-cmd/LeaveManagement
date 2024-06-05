using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    /// <summary>
    /// Controller for managing user accounts and passwords.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Sends a password reset OTP to the user's email.
        /// </summary>
        /// <param name="model">The model containing the user's email.</param>
        /// <returns>A message indicating the success of the operation.</returns>
        [HttpPost("send-password-reset-otp")]
        public async Task<IActionResult> SendPasswordResetOtp([FromBody] SendOtpModel model)
        {
            await _userService.SendPasswordResetOtpAsync(model.Email);
            return Ok("Password reset OTP sent. Please check your email.");
        }

        /// <summary>
        /// Verifies the OTP sent to the user's email for password reset.
        /// </summary>
        /// <param name="model">The model containing the user's email and OTP.</param>
        /// <returns>A message indicating the success of the operation or an error message if the OTP is invalid.</returns>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpModel model)
        {
            var result = await _userService.VerifyOtpAsync(model.Email, model.Otp);
            if (!result)
            {
                return Unauthorized("Invalid OTP.");
            }
            return Ok("OTP verified successfully.");
        }

        /// <summary>
        /// Resets the user's password.
        /// </summary>
        /// <param name="model">The model containing the user's email and new password.</param>
        /// <returns>A message indicating the success of the operation.</returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            await _userService.ResetPasswordAsync(model);
            return Ok("Password reset successfully.");
        }
    }
}
