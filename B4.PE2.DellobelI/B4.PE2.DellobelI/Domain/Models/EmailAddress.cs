using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE2.DellobelI.Domain.Models
{
    public class EmailAddress
    {
        private string email;
        private string naam;

        public string Email { get; set; }
        public string Naam { get; set; }

        public EmailAddress(string email, string naam)
        {
            Email = email;
            Naam = naam;
        }

        public void GetEmailAdress()
        {

        }
    }
}
