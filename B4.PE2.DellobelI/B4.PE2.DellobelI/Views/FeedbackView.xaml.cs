using B4.PE2.DellobelI.Domain.Models;
using B4.PE2.DellobelI.Domain.Services;
using B4.PE2.DellobelI.Domain.Validators;
using B4.PE2.DellobelI.ApiKey;
using FluentValidation;
using Plugin.Messaging;
using System;
using System.Net;
using System.Threading.Tasks;
using Windows.Web.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE2.DellobelI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackView : ContentPage

    {

        FeedbackInMemoryService feedbackInMemory;
        private Feedback currentFeedback;
        private IValidator feedbackValidator;

        public FeedbackView()
        {
            InitializeComponent();
            feedbackInMemory = new FeedbackInMemoryService();
            feedbackValidator = new FeedBackValidator();


        }

        private async void btnZendFeedback_Clicked(object sender, EventArgs e)
        {
            SaveFeedbackState();
            if (Validate(currentFeedback))
            {
                await feedbackInMemory.SaveFeedbackLijst(currentFeedback);
                var answer = await DisplayAlert("Feedback", $"Mag uw vraag over:\n{currentFeedback.GetPickListOnderwerp}\nverstuurd worden?", "OK", "No,Wait!");
                if (answer == true)
                {
                    await Navigation.PopToRootAsync();
                   

                    //Verouderd
                    //var EmailTask = MessagingPlugin.EmailMessenger;

                    //EmailTask.SendEmail($"ivan.dellobel@gmail.com", $" Onderwerp = {currentFeedback.GetPickListOnderwerp}",
                    //    $"\n Naam = {currentFeedback.Naam}" +
                    //    $"\n Afzender = {currentFeedback.Email}" +
                    //    $"\n Telefoon = {currentFeedback.Telefoonnummer}" +
                    //    $"\n Geboortedatum = {currentFeedback.Geboortedatum}" +
                    //    $" Onderwerp = {currentFeedback.GetPickListOnderwerp}" +
                    //    $"\n Bericht = {currentFeedback.Bericht}"
                    //   );

                    //Niet de professionele werkwijze, gewijzigd met dependency emailservice!
                    //var emailMessenger = CrossMessaging.Current.EmailMessenger;
                    //if (emailMessenger.CanSendEmail)
                    //{
                    //    // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                    //    emailMessenger.SendEmail("ivan.dellobel@gmail.com", $" Onderwerp = {currentFeedback.GetPickListOnderwerp}",
                    //                            $"\n Naam = {currentFeedback.Naam}" +
                    //                            $"\n Afzender = {currentFeedback.Email}" +
                    //                            $"\n Telefoon = {currentFeedback.Telefoonnummer}" +
                    //                            $"\n Geboortedatum = {currentFeedback.Geboortedatum}" +
                    //                            $"\n Onderwerp = {currentFeedback.GetPickListOnderwerp}" +
                    //                            $"\n Bericht = {currentFeedback.Bericht}"
                    //                             );

                        // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc. 
                        //var email = new EmailMessageBuilder()
                        //  .To("ivan.dellobel@gmail.com")
                        //  .Cc("ivan.dellobel@telenet.be")
                        //  .Bcc(new[] { "natalja.oestinskaja@gmail.com", "natalja.oestinskaja@telenet.be" })
                        //  .Subject($" Onderwerp = {currentFeedback.GetPickListOnderwerp}")
                        //  .Body($"\n Naam = {currentFeedback.Naam}" +
                        //        $"\n Afzender = {currentFeedback.Email}" +
                        //        $"\n Telefoon = {currentFeedback.Telefoonnummer}" +
                        //        $"\n Geboortedatum = {currentFeedback.Geboortedatum}" +
                        //        $"\n Onderwerp = {currentFeedback.GetPickListOnderwerp}" +
                        //        $"\n Bericht = {currentFeedback.Bericht}")
                        //  .Build();

                        //emailMessenger.SendEmail(email);

                    }
                   

                   var emailService = DependencyService.Get<EmailService>();
                    
                   await emailService.SendMailAsync(
                                    $" Onderwerp = {currentFeedback.GetPickListOnderwerp}",
                                     $"\n Naam = {currentFeedback.Naam}" +
                                                $"\n Afzender = {currentFeedback.Email}" +
                                                $"\n Telefoon = {currentFeedback.Telefoonnummer}" +
                                                $"\n Geboortedatum = {currentFeedback.Geboortedatum}" +
                                                $"\n Onderwerp = {currentFeedback.GetPickListOnderwerp}" +
                                                $"\n Bericht = {currentFeedback.Bericht}",
                                   "ivan.dellobel@gmail.com",
                                   "Ivan Dellobel",
                                    currentFeedback.Email,
                                    currentFeedback.Naam
                                    
                                    );
               

                await DisplayAlert("Bericht is verzonden", $"Beste {currentFeedback.Naam},\nUw bericht met onderwerp:\n{currentFeedback.GetPickListOnderwerp}"
                                           + $" wordt zo spoedig mogelijk behandeld.\n"
                                           + $"Vriendelijke groeten\nIvan ", "OK");

            }

        }

        private void SaveFeedbackState()
        {
            currentFeedback = new Feedback();
            currentFeedback.Naam = txtNaamEnVoornaam.Text;
            currentFeedback.Email = txtEmail.Text;
            currentFeedback.Telefoonnummer = txtTelefoon.Text;
            currentFeedback.GetPickListOnderwerp = picOnderwerp.SelectedItem.ToString();
            currentFeedback.Geboortedatum = txtGeboortedatum.Text;
            currentFeedback.Bericht = txtBericht.Text;
        }

        protected override void OnAppearing()
        {
            //Laad een ingevuld formulier in, om het niet altijd manueel te moeten invullen.
            LoadFeedbackState();
            base.OnAppearing();
        }

        private async void LoadFeedbackState()
        {
            var Loadfeedback = await feedbackInMemory.GetFeedbackLijst(Guid.Empty);

            txtNaamEnVoornaam.Text = Loadfeedback.Naam;
            txtEmail.Text = Loadfeedback.Email;
            txtTelefoon.Text = Loadfeedback.Telefoonnummer;
            txtGeboortedatum.Text = Loadfeedback.Geboortedatum;
            picOnderwerp.SelectedIndex = 1;
            txtBericht.Text = Loadfeedback.Bericht;
        }

        private bool Validate(Feedback feedback)
        {
            lblErrorName.IsVisible = false;
            lblErrorEmail.IsVisible = false;
            lblErrorTelefoon.IsVisible = false;
            lblErrorGeboortedatum.IsVisible = false;
            lblErrorOnderwerp.IsVisible = false;
            lblErrorBericht.IsVisible = false;

            var validationResult = feedbackValidator.Validate(feedback);
            feedback.GetPickListOnderwerp = picOnderwerp.SelectedItem.ToString();

            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(feedback.Naam))
                {
                    lblErrorName.Text = error.ErrorMessage;
                    lblErrorName.IsVisible = true;
                }

                if (error.PropertyName == nameof(feedback.Email))
                {
                    lblErrorEmail.Text = error.ErrorMessage;
                    lblErrorEmail.IsVisible = true;
                }

                if (error.PropertyName == nameof(feedback.Telefoonnummer))
                {
                    lblErrorTelefoon.Text = error.ErrorMessage;
                    lblErrorTelefoon.IsVisible = true;
                }

                if (error.PropertyName == nameof(feedback.Geboortedatum))
                {
                    lblErrorGeboortedatum.Text = error.ErrorMessage;
                    lblErrorGeboortedatum.IsVisible = true;
                }

                if (error.PropertyName == nameof(feedback.GetPickListOnderwerp))
                {
                    lblErrorOnderwerp.Text = error.ErrorMessage;
                    lblErrorOnderwerp.IsVisible = true;
                }

                if (error.PropertyName == nameof(feedback.Bericht))
                {
                    lblErrorBericht.Text = error.ErrorMessage;
                    lblErrorBericht.IsVisible = true;
                }
            }
            return validationResult.IsValid;
        }


    }
}
