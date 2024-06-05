using DomainLayer.Interface.IRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DomainLayer.AcessLayer;
using DomainLayer.ViewModels;
using BusinessLayer.Helper;

/// <summary>
/// Service class for user-related operations.
/// </summary>
public class UserService
{
    private readonly IUnitOfWork _context;
    private readonly IEmailRepo _emailSender;
    private readonly OtpService _otpService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="context">The unit of work context.</param>
    /// <param name="emailSender">The email sender service.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="otpService">The OTP service.</param>
    public UserService(IUnitOfWork context, IEmailRepo emailSender, IConfiguration configuration, OtpService otpService)
    {
        _context = context;
        _otpService = otpService;

        // Initialize email sender with IConfiguration directly
        _emailSender = new EmailSender(configuration);
    }

    /// <summary>
    /// Sends the password reset OTP to the email.
    /// </summary>
    /// <param name="email">The email address to which the OTP will be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendPasswordResetOtpAsync(string email)
    {
        var user = await _context.Context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        // Generate OTP
        string otp = _otpService.GenerateOtp();

        // Set OTP and expiry
        user.PasswordResetToken = otp;
        user.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(1);

        // Save changes to the database
        await _context.Context.SaveChangesAsync();

        // Construct the email content
        var subject = "Your Password Reset OTP Code";
        var message = $"Your OTP code is: {otp}. It is valid for the next 10 minutes.";

        // Send the email
        await _emailSender.SendEmailAsync(email, subject, message);
    }

    /// <summary>
    /// Verifies the OTP from the mail.
    /// </summary>
    /// <param name="email">The email address to verify OTP for.</param>
    /// <param name="otp">The OTP code to verify.</param>
    /// <returns>True if the OTP is valid; otherwise, false.</returns>
    public async Task<bool> VerifyOtpAsync(string email, string otp)
    {
        var user = await _context.Context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null || user.PasswordResetToken != otp || user.PasswordResetTokenExpiry < DateTime.UtcNow)
        {
            return false;
        }

        // OTP is valid, clear it
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = null;
        await _context.Context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Resets the password and saves it as hash.
    /// </summary>
    /// <param name="reset">The password reset model containing email and new password.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task ResetPasswordAsync(ResetPasswordModel reset)
    {
        var user = await _context.Context.Users.SingleOrDefaultAsync(u => u.Email == reset.Email);

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        string hashedPassword = PasswordHash.Hashing(reset.NewPassword);
        user.Password = hashedPassword;

        await _context.Context.SaveChangesAsync();
    }
}
