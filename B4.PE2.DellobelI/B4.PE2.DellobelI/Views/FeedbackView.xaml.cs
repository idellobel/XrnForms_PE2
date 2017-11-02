using B4.PE2.DellobelI.Domain.Models;
using B4.PE2.DellobelI.Domain.Services;
using B4.PE2.DellobelI.Domain.Validators;
using FluentValidation;
using System;
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
            if(Validate(currentFeedback))
            {
            await feedbackInMemory.SaveFeedbackLijst(currentFeedback);
            var answer = await DisplayAlert("Feedback", $"Mag uw vraag over:\n{currentFeedback.GetPickListOnderwerp}\nverstuurd worden?", "OK", "No,Wait!");
            if(answer == true)
            {
                await Navigation.PopToRootAsync();
                await DisplayAlert("Bericht is verzonden", $"Beste {currentFeedback.Naam},\nUw bericht met onderwerp:\n{currentFeedback.GetPickListOnderwerp}"
                                    + $" wordt zo spoedig mogelijk behandeld.\n"
                                    + $"Vriendelijke groeten\nIvan ", "OK");
            }
           
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

        protected  override void OnAppearing()
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
                if(error.PropertyName == nameof(feedback.Naam))
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
