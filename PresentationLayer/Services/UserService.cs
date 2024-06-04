using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DomainLayer.AcessLayer;
using DomainLayer.Interface.IRepo;
using BusinessLayer.Helper;
using DomainLayer.ViewModels;

public class UserService
{
    /// <summary>
    /// impelementation of  password reset sending servive using send grid
    /// </summary>
    private readonly IUnitOfWork _context;
    private readonly IEmailRepo _emailSender;
    private readonly IConfiguration _configuration;
    private readonly OtpService _otpService;

    public UserService(IUnitOfWork context, IEmailRepo emailSender, IConfiguration configuration, OtpService otpService)
    {
        _context = context;
        _emailSender = emailSender;
        _configuration = configuration;
        _otpService = otpService;
    }
    /// <summary>
    /// this send the password reste otp to email 
    /// </summary>
    /// <param name="email"> this takes the email</param>
    /// <returns>email format</returns>
    /// <exception cref="Exception"> if user mail not found</exception>
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
    /// this verfy the otp from the mail
    /// </summary>
    /// <param name="email"> use email of user</param>
    /// <param name="otp"> for opotional </param>
    /// <returns></returns>
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
    /// this reset the password and save it as hash 
    /// </summary>
    /// <param name="reset">this take viewmodel pasrameter </param>
    /// <returns>save password</returns>
    /// <exception cref="Exception">if user is not found</exception>
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
