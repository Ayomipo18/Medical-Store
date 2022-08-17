using Hangfire;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utils.Email
{
    public class EmailManager : IEmailManager
    {
        private readonly SendGridClient _clientKey;
        private readonly IConfiguration _configuration;
        private readonly EmailAddress _from;
        public EmailManager(IConfiguration configuration)
        {
            _configuration = configuration;
            var sendGridKey = configuration["SENDGRID_KEY"];
            var senderEmail = configuration["SENDER_EMAIL"];
            _clientKey = new SendGridClient(sendGridKey);
            _from = new EmailAddress(senderEmail);
        }

        public void SendSingleEmail(string receiverAddress, string message, string subject)
        {
            BackgroundJob.Enqueue(() => SendSingleMail(receiverAddress, message, subject));
        }

        public async Task SendSingleMail(string receiverAddress, string message, string subject)
        {
            var To = new EmailAddress(receiverAddress);
            var plainText = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(_from, To, subject, plainText, htmlContent);
            var response = await _clientKey.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
                throw new RestException(response.StatusCode, "Email failed to send");
        }
    }
}
