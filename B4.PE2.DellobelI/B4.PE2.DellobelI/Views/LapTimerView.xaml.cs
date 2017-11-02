using B4.PE2.DellobelI.Domain.Models;
using B4.PE2.DellobelI.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE2.DellobelI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LapTimerView : ContentPage
    {
        //RondetijdenService rondetijdenListService;
        public static Stopwatch sw = new Stopwatch();
        //bool pageVisible = false;
        string tijd;
        public string Laptime { get; set; }
        public DateTime StartTime;
        public DateTime StopTime;
        private string timepassed;

        public LapTimerView()
        {
            //rondetijdenListService = new RondetijdenService();
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("TimerTijdPage_TotaleTijd"))
                tijd = (string)Application.Current.Properties["TimerTijdPage_TotaleTijd"];
            btnLap.IsVisible = false;
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


            ResetListview();
            UpdateDisplay();

        }

        private void BtnStartStop_Click(object sender, EventArgs e)
        {

            Stopwatch();

        }

        private string UpdateDisplay()
        {
            Application.Current.Properties["TimerTijdPage_TotaleTijd"] = tijd;
            Application.Current.SavePropertiesAsync();
            Label time = new Label();
            time.Text = $"{sw.Elapsed.Minutes.ToString("00")}:" +
            $"{sw.Elapsed.Seconds.ToString("00")}," +
            $"{sw.Elapsed.Milliseconds.ToString("000")}";
            return time.Text;
        }

        private void Stopwatch()
        {
            if (!sw.IsRunning)
            {
                btnLap.IsVisible = true;
                sw.Start();
                StartTime = DateTime.Now;
                btnStartStop.Text = "\u2588";
                btnStartStop.BackgroundColor = Color.Red;
                btnStartStop.TextColor = Color.White;
                
                btnReset.IsVisible = false;

            }
            else
            {
                sw.Stop();
                StopTime = DateTime.Now;
                btnStartStop.Text = "\u25B6";
                btnStartStop.BackgroundColor = Color.Green;
                btnStartStop.TextColor = Color.White;
                btnStartStop.IsEnabled = false;
                btnLap.IsVisible = false;
                btnReset.IsVisible = true;
                AddItemToListview();
            }
        }

        protected override void OnAppearing()
        {

            
            //pageVisible = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {
                lblTime.Text = UpdateDisplay();
                return true;
            }
            );

            if (sw.IsRunning)
            {
                btnStartStop.Text = "\u2588";
                btnStartStop.BackgroundColor = Color.Red;
                btnStartStop.TextColor = Color.White;

            }
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            //pageVisible = false;
            base.OnDisappearing();
        }

        private int index = 0;

        private void btnLap_Clicked(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                AddItemToListview();
                StartTime = DateTime.Now;
            }
            else
            {
                ResetListview();
            }
        }

        public string GetTimePassed()
        {
            TimeSpan t;
            Laptime = $"{t.Minutes.ToString("00")}:" +
                        $"{t.Seconds.ToString("00")}," +
                        $"{t.Milliseconds.ToString("000")}";

            if (sw.IsRunning)
            {
                t = (DateTime.Now - StartTime);
                return $"{t.Minutes.ToString("00")}:" +
                        $"{t.Seconds.ToString("00")}," +
                        $"{t.Milliseconds.ToString("000")}"; ;

            }
            else
            {
                t = (StopTime - StartTime);
                return $"{t.Minutes.ToString("00")}:" +
                        $"{t.Seconds.ToString("00")}," +
                        $"{t.Milliseconds.ToString("000")}"; ;
            }

        }

        private static ObservableCollection<Lap> rondetijd = new ObservableCollection<Lap>();

        private void ResetListview()
        {
            rondetijd = new ObservableCollection<Lap>();
            lvRondeLijst.ItemsSource = rondetijd;
            index = 0;
        }
        private void AddItemToListview()
        {
            timepassed = GetTimePassed();
            index++;
            string testresult = $"Rondetijd ronde {index} is: {timepassed} ";
            rondetijd.Add(new Lap() { DisplayName = testresult });
            lvRondeLijst.ItemsSource = rondetijd;
        }
    }


}