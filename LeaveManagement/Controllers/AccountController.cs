using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("send-password-reset-otp")]
    public async Task<IActionResult> SendPasswordResetOtp([FromBody] SendOtpModel model)
    {
        await _userService.SendPasswordResetOtpAsync(model.Email);
        return Ok("Password reset OTP sent. Please check your email.");
    }

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

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
    {
        await _userService.ResetPasswordAsync(model);
        return Ok("Password reset successfully.");
    }
}
