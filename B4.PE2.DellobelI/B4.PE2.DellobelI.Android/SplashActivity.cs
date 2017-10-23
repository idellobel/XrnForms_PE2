using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Content.PM;
using Android.Views;
using Android.Widget;

namespace B4.PE2.DellobelI.Droid
{
    [Activity(Label = "Timer en Form", Icon = "@drawable/icon",
        Theme = "@style/MainTheme.Splash", MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   
    public class SplashActivity : Android.Support.V7.App.AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}