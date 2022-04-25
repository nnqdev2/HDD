﻿using Microsoft.Extensions.Options;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using global::HDD.Email;

namespace HDD.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly EmailOptions _options;

        public EmailSender(ILoggerFactory loggerFactory, IOptions<EmailOptions> options)
        {
            _logger = loggerFactory.CreateLogger<EmailSender>();
            _options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_options.AdminEmail);
            mailMessage.To.Add(email);
            mailMessage.Body = message;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;

            using var client = new SmtpClient(_options.MailServer, _options.MailPort);
            client.Credentials = new System.Net.NetworkCredential(_options.MailUserId, _options.MailPassword);
            await client.SendMailAsync(mailMessage);
            //_logger.LogInformation(true
            //                       ? $"Email to {email} queued successfully!"
            //                       : $"Failure Email to {email}");
        }
    }
}
