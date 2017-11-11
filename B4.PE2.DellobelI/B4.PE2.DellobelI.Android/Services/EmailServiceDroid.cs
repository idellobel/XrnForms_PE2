using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using B4.PE2.DellobelI.Domain.Services;
using Xamarin.Forms;
using System.Net.Mail;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;
using B4.PE2.DellobelI.ApiKey;
using System.Threading.Tasks;

[assembly: Dependency(typeof(B4.PE2.DellobelI.Droid.Services.EmailServiceDroid))]

namespace B4.PE2.DellobelI.Droid.Services
{
    public class EmailServiceDroid : EmailService
    {

        public override Task SendMailAsync(string subject, string body, string mailTo, string naamTo, string mailFrom, string naamFrom)

        {
            

            var apiKey = new ApiKey.ApiKey();
            var client = new SendGridClient(apiKey.Key);
            var msg = new SendGridMessage();
            var From = new SendGrid.Helpers.Mail.EmailAddress(mailFrom,naamFrom) ;
            var To = new SendGrid.Helpers.Mail.EmailAddress(mailTo, naamTo);
            var plainTextContent = body;
            var htmlContent = body;
            msg = MailHelper.CreateSingleEmail(From, To, subject, plainTextContent, htmlContent);

            return client.SendEmailAsync(msg);

        }




    }
}