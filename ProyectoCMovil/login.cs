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

namespace ProyectoCMovil
{
    [Activity(Label = "Login")]
    public class login : Activity 
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.loginLayout);
                     
            Button button = FindViewById<Button>(Resource.Id.logBtn);
            button.Click += delegate { StartActivity(typeof(Map)); };
        }
    }
}