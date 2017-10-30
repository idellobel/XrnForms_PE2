using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

namespace B4.PE2.DellobelI.Domain.Models
{
    public class TimerClass
    {
        public DateTime StartTime;
        public DateTime StopTime;
        private  Stopwatch sw = new Stopwatch();
        public string laptime;
        public string watchtime;
       
        //public TimerClass()
        //{
        //    if (Application.Current.Properties.ContainsKey("TimerTijdPage_TotaleTijd"))
        //        watchtime = (string)Application.Current.Properties["TimerTijdPage_TotaleTijd"];
        //    if (Application.Current.Properties.ContainsKey("TimerTijdPage_RondeTijd"))
        //        laptime = (string)Application.Current.Properties["TimerTijdPage_RondeTijd"];
        //}

        public  Stopwatch Sw { get => sw; set => sw = value; }

        public void Stopwatch()
        {
            if (!Sw.IsRunning)
            {
                Sw.Start();
                StartTime = DateTime.Now;
            }
            else
            {
                Sw.Stop();
                StopTime = DateTime.Now;
            }
        }

        public void Reset()
        {
            if (Sw.IsRunning)
            {
                Sw.Restart();
            }
            else
            {
                Sw.Reset();
            }
        }

        public string GetTimePassed()
        {
            TimeSpan t;
             laptime = $"{t.Minutes.ToString("00")}:" +
                        $"{t.Seconds.ToString("00")}," +
                        $"{t.Milliseconds.ToString("000")}";
            if (Sw.IsRunning)
            {
                t = (DateTime.Now - StartTime);
                return laptime;
                
            }
            else
            {

                t = (StopTime - StartTime);
                return laptime;
            }
            //Application.Current.Properties["TimerTijdPage_RondeTijd"] = laptime;
            //Application.Current.SavePropertiesAsync();


        }
        public string UpdateDisplay()
        {
            
            Label time = new Label
            {
                Text = $"{Sw.Elapsed.Minutes.ToString("00")}:" +
            $"{Sw.Elapsed.Seconds.ToString("00")}," +
            $"{Sw.Elapsed.Milliseconds.ToString("000")}"
            };

            //Application.Current.Properties["TimerTijdPage_TotaleTijd"] = watchtime;
            //Application.Current.SavePropertiesAsync();
            return time.Text;
        }
    }

   
}
