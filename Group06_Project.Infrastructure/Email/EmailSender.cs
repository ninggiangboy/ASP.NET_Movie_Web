using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Group06_Project.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    private readonly string _password;
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _username;

    public EmailSender(IConfiguration configuration)
    {
        _username = configuration["Mail:Username"];
        _password = configuration["Mail:Password"];
        _smtpHost = configuration["Mail:SmtpHost"];
        _smtpPort = Convert.ToInt32(configuration["Mail:SmtpPort"]);
    }

    public async Task SendEmailAsync(string emailTo, string subject, string htmlMessage)
    {
        var email = new MimeMessage
        {
            From = { MailboxAddress.Parse(_username) },
            To = { MailboxAddress.Parse(emailTo) },
            Subject = subject,
            Body = new TextPart(TextFormat.Html)
            {
                Text = htmlMessage
            }
        };
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_smtpHost, _smtpPort, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_username, _password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}