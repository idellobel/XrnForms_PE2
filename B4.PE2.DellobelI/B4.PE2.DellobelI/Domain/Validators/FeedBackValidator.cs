using B4.PE2.DellobelI.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE2.DellobelI.Domain.Validators
{
    public class FeedBackValidator : AbstractValidator<Feedback>
    {
        public FeedBackValidator()
        {
            RuleFor(feedback => feedback.Naam)
                .NotEmpty()
                .WithMessage("Veld 'Naam' mag niet leeg zijn");

            RuleFor(feedback => feedback.Email)
               .NotEmpty()
               .WithMessage("Veld 'E-Mail' mag niet leeg zijn")
               .Length(3, 50)
               .EmailAddress()
               .WithMessage("Vul een geldig mail-adres in");

            RuleFor(feedback => feedback.Geboortedatum)
                .NotEmpty()
                .WithMessage("Veld 'Geboortedatum' mag niet leeg zijn");


            RuleFor(feedback => feedback.Telefoonnummer)
                .NotEmpty()
                .WithMessage("Veld 'Telefoonnummer' mag niet leeg zijn");


            RuleFor(feedback => feedback.GetPickListOnderwerp)
               .NotEmpty()
               .WithMessage("Selecteer een onderwerp")
               .Must(BeDifferentThan)
               .WithMessage("Kies een onderwerp uit de lijst...");

            RuleFor(feedback => feedback.Bericht)
               .NotEmpty()
               .WithMessage("Vul de 'berichttekst' in");
        }

        private bool BeDifferentThan(string GetPickListOnderwerp)
        {
            var itemlist = GetPickListOnderwerp;
                return (itemlist != "Kies uit volgende lijst...");
        }

      
    }
}
