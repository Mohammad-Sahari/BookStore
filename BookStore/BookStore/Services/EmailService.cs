using BookStore.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;



namespace BookStore.Services
{
    public class EmailService : IEmailService
    {


        private const string templatePath = @"EmailTemplate/{0}.html";


        private readonly SMTPConfigModel _smtpConfig;
        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        public async Task SendTestEmail(UserEmailOptions emailOptions)
        {
            emailOptions.Subject =UpdatePlaceHolders("This is test email subject from bookstore",emailOptions.PlaceHolders);
            emailOptions.Body =UpdatePlaceHolders(GetEmailBody("TestEmail"),emailOptions.PlaceHolders);

            await SendEmail(emailOptions);
        }
        public async Task SendConfirmationEmail(UserEmailOptions emailOptions)
        {
            emailOptions.Subject =UpdatePlaceHolders("Hello {{UserName}}, Confirm your email account",emailOptions.PlaceHolders);
            emailOptions.Body =UpdatePlaceHolders(GetEmailBody("EmailConfirm"),emailOptions.PlaceHolders);

            await SendEmail(emailOptions);
        }

        public async Task SendForgotPasswordEmail(UserEmailOptions emailOptions)
        {
            emailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}}, Reset your password", emailOptions.PlaceHolders);
            emailOptions.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), emailOptions.PlaceHolders);

            await SendEmail(emailOptions);
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML,
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.Username, _smtpConfig.Password);


            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential

            };


            mail.BodyEncoding = Encoding.Default;

           await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string,string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach(var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return text;
        }
    }
}
