using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using B4.PE2.DellobelI.Domain.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using SendGrid;
using SendGrid.Helpers.Mail;

[assembly: Dependency(typeof(B4.PE2.DellobelI.UWP.Services.EmailServiceUWP))]

namespace B4.PE2.DellobelI.UWP.Services
{
    public class EmailServiceUWP : EmailService
    {
        public override Task SendMailAsync(string subject, string body, string mailTo, string naamTo, string mailFrom, string naamFrom)

        {


            var apiKey = new ApiKey.ApiKey();
            var client = new SendGridClient(apiKey.Key);
            var msg = new SendGridMessage();
            var From = new SendGrid.Helpers.Mail.EmailAddress(mailFrom, naamFrom);
            var To = new SendGrid.Helpers.Mail.EmailAddress(mailTo, naamTo);
            var plainTextContent = body;
            var htmlContent = body;
            msg = MailHelper.CreateSingleEmail(From, To, subject, plainTextContent, htmlContent);

            return client.SendEmailAsync(msg);

        }

    }
}
