using System;
using System.Diagnostics;
using System.Threading;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE2.DellobelI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerView : ContentPage
    {
        public static Stopwatch sw = new Stopwatch();
        //bool pageVisible = false;
        string tijd;

        public TimerView()
        {
            InitializeComponent();

            if (Application.Current.Properties.ContainsKey("TimerTijdPage_TotaleTijd"))
                tijd = (string)Application.Current.Properties["TimerTijdPage_TotaleTijd"];

            UpdateDisplay();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                sw.Restart();
            }
            else
            {
                sw.Reset();
                btnStartStop.IsEnabled = true;
            }
            Application.Current.Properties["TimerTijdPage_TotaleTijd"] = null;
            Application.Current.SavePropertiesAsync();
            UpdateDisplay();
        }


        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            Stopwatch();
        }


        private void UpdateDisplay()
        {
            lblTime.Text = tijd;

            tijd = $"{sw.Elapsed.Minutes.ToString("00")}:" +
            $"{sw.Elapsed.Seconds.ToString("00")}," +
            $"{sw.Elapsed.Milliseconds.ToString("000")}";

            Application.Current.Properties["TimerTijdPage_TotaleTijd"] = tijd;
            Application.Current.SavePropertiesAsync();

        }
        private void Stopwatch()
        {
            if (!sw.IsRunning)
            {
                sw.Start();

                btnStartStop.Text = "\u2588";
                btnStartStop.BackgroundColor = Color.Red;
                btnStartStop.TextColor = Color.White;


            }
            else
            {
                sw.Stop();
                btnStartStop.Text = "\u25B6";
                btnStartStop.BackgroundColor = Color.Green;
                btnStartStop.TextColor = Color.White;
                btnStartStop.IsEnabled = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //pageVisible = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {

                UpdateDisplay();
                return true;
            }

                );
            if (sw.IsRunning)
            {
                btnStartStop.Text = "\u2588";
                btnStartStop.BackgroundColor = Color.Red;
                btnStartStop.TextColor = Color.White;
            }
        }
        protected override void OnDisappearing()
        {
            //pageVisible = false;
            base.OnDisappearing();
        }


    }
}
