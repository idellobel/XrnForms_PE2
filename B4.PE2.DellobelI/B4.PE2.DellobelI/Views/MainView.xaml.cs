using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE2.DellobelI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        private async void Timer_Tapped(object sender, EventArgs e)
        {
            busyindicator.IsVisible = true;
            await Task.Delay(Constants.Mocking.BusyIndicator); //controle op werking busyindicator
            await Navigation.PushAsync(new TimerView());
            busyindicator.IsVisible = false;
        }

        private async void Laptimer_Tapped(object sender, EventArgs e)
        {
            busyindicator.IsVisible = true;
            await Task.Delay(Constants.Mocking.BusyIndicator);
            await Navigation.PushAsync(new LapTimerView());
            busyindicator.IsVisible = false;
        }

        private async void Form_Tapped(object sender, EventArgs e)
        {
            busyindicator.IsVisible = true;
            await Task.Delay(Constants.Mocking.BusyIndicator);
            await Navigation.PushAsync(new FeedbackView());
            busyindicator.IsVisible = false;
        }
    }
}