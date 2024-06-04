using System.Security.Cryptography;
using DomainLayer.Interface.IRepo;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;



public class EmailSender : IEmailRepo
{
    /// <summary>
    /// this is for emial sending and otp service genration
    /// </summary>
    private readonly string _sendGridApiKey;

    public EmailSender(IConfiguration configuration)
    {
        _sendGridApiKey = configuration["SendGridApiKey"];
    }
    /// <summary>
    /// this send the email to the particular mail for reset
    /// </summary>
    /// <param name="email">thuus is email </param>
    /// <param name="subject">subject for email</param>
    /// <param name="message">meassage of emial </param>
    /// <returns>sends email</returns>
    /// <exception cref="Exception">if the email sned failed</exception>
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SendGridClient(_sendGridApiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("parlad@gmail.com", "PraladRaya"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(email));

        var response = await client.SendEmailAsync(msg);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to send email");
        }
    }
}

/// <summary>
/// generated otp for sendoing though email user service
/// </summary>
public class OtpService
{
    public string GenerateOtp()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}