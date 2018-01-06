using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MovieApp;
using MovieApp.Droid;

namespace HelloWorld.Droid
{
    [Activity(Label = "MovieApp", Theme = "@style/MyTheme.Splash", MainLauncher = true, Icon = "@mipmap/icon")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create API here
            MainActivity.api = new MovieApi();

            this.StartActivity(typeof(MainActivity));
            this.Finish();
        }
    }
}