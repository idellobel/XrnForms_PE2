using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace B4.PE2.DellobelI.Domain.Models
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }
        public string Geboortedatum { get; set; }
        public string GetPickListOnderwerp { get; set; }
        public int GetSelectedIndexPicker { get; set; }
        public string Bericht { get; set; }
        public ICollection<Onderwerp> PickListOnderwerp { get; set; }

    }
}
