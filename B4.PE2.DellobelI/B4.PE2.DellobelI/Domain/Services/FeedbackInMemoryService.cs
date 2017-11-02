using B4.PE2.DellobelI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE2.DellobelI.Domain.Services
{
    public class FeedbackInMemoryService
    {
        private static List<Feedback> feedbackLijst = new List<Feedback>
        {
            new Feedback
            {
                Id = Guid.Empty,
                OwnerId = Guid.Empty,
                Naam = "Dellobel Ivan",
                Email= "ivan.dellobel@gmail.com", 
                Geboortedatum = "14 mei 1962",
                Telefoonnummer= "058 594566",
                Bericht="Dit is een bericht bestemd voor de ontwikkelaar over het onderwerp fakturering en verkoop. De applicatie werd reeds betaald, maar faktuur is niet terecht? ",
                GetPickListOnderwerp = "Fakturering/Verkoop",
                PickListOnderwerp = new List<Onderwerp>
                {
                    new Onderwerp {
                        Id = Guid.NewGuid(), OnderwerpNaam= "Fakturering/Verkoop", VolgNummer= 1 },
                     new Onderwerp {
                        Id = Guid.NewGuid(), OnderwerpNaam= "Technisch defect", VolgNummer= 2 },
                      new Onderwerp {
                        Id = Guid.NewGuid(), OnderwerpNaam= "Andere", VolgNummer= 3 }
                }

            }
        };
        public async Task<IEnumerable<Feedback>> GetFeedbackLijstForUser(Guid userid)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return feedbackLijst.Where(f => f.OwnerId == userid);
        }

        public async Task<Feedback> GetFeedbackLijst(Guid feedbackId)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return feedbackLijst.FirstOrDefault(f => f.Id == feedbackId);
        }

        public async Task SaveFeedbackLijst(Feedback feedback)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            var savedFeedback = feedbackLijst.FirstOrDefault(f => f.Id == feedback.Id);
            if (savedFeedback == null) //this is a new bucket
            {
                savedFeedback = feedback;
                savedFeedback.Id = Guid.NewGuid();
                feedbackLijst.Add(savedFeedback);
            }
            savedFeedback.Naam= feedback.Naam;
            savedFeedback.OwnerId = feedback.OwnerId ;
            savedFeedback.GetPickListOnderwerp = feedback.GetPickListOnderwerp;
            savedFeedback.Telefoonnummer = feedback.Telefoonnummer;
            savedFeedback.Geboortedatum= feedback.Geboortedatum;
            savedFeedback.Email = feedback.Email;
           
        }
        public async Task DeleteFeedbackLijst(Guid feedbackId)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            var feedback = feedbackLijst.FirstOrDefault(f => f.Id == feedbackId);
            feedbackLijst.Remove(feedback); }
        }

    
}
