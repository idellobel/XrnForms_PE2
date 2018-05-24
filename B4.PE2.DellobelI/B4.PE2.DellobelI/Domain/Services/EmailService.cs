using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace B4.PE2.DellobelI.Domain.Services
{
    public abstract class EmailService
    {
        
        
        private static readonly Lazy<EmailService> instance = new Lazy<EmailService>(() => DependencyService.Get<EmailService>());

        public static Lazy<EmailService> Instance => instance;

        //public abstract bool CanSend { get; }

        public abstract Task SendMailAsync(string subject, string body, string mailTo, string naamTo, string mailFrom, string naamFrom);

       
    }
}
