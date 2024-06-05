using System.Net;
using System.Net.Mail;
using DomainLayer.Interface.IRepo;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Service for sending emails using SMTP.
/// </summary>
public class EmailSender : IEmailRepo
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailSender"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <exception cref="ArgumentNullException">Thrown when SMTP configuration is not complete.</exception>
    public EmailSender(IConfiguration configuration)
    {
        _smtpHost = configuration["Smtp:Host"];
        _smtpPort = int.Parse(configuration["Smtp:Port"]);
        _smtpUsername = configuration["Smtp:Username"];
        _smtpPassword = configuration["Smtp:Password"];

        if (string.IsNullOrWhiteSpace(_smtpHost) || string.IsNullOrWhiteSpace(_smtpUsername) || string.IsNullOrWhiteSpace(_smtpPassword))
        {
            throw new ArgumentNullException(nameof(configuration), "SMTP configuration is not complete.");
        }
    }

    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <param name="subject">The email subject.</param>
    /// <param name="message">The email message body.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when email, subject, or message is null or empty.</exception>
    /// <exception cref="Exception">Thrown when sending email fails.</exception>
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
        if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentNullException(nameof(subject));
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

        using (var client = new SmtpClient(_smtpHost, _smtpPort))
        using (var mailMessage = new MailMessage())
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            client.EnableSsl = true;

            mailMessage.From = new MailAddress(_smtpUsername);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(email);

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }
    }
}

/// <summary>
/// Service for generating OTP (One-Time Password).
/// </summary>
public class OtpService
{
    /// <summary>
    /// Generates a random OTP.
    /// </summary>
    /// <returns>The generated OTP as a string.</returns>
    public string GenerateOtp()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}
