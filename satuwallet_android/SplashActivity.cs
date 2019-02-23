using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using satuwallet_android.Activities;
using satuwallet_android.Helpers;
using satuwallet_android.Models;

namespace satuwallet_android
{
    [Activity(Label = "@string/app_name", Theme = "@style/splashscreen", MainLauncher = true)]
    public class SplashActivity : AppCompatActivity
    {
        // Launcher icon generator: https://romannurik.github.io/AndroidAssetStudio/icons-launcher.html

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.activity_main);

            DbContext.InitialiseDatabase();

            //var i = new Intent(Application.Context, typeof(LoginActivity));
            //StartActivity(i);
            //Finish();
        }
        
        protected override void OnStart()
        {
            var isLoggedIn = IsStillLoggedIn();
            if (isLoggedIn)
            {
                var i = new Intent(Application.Context, typeof(MainActivity));
                StartActivity(i);
                FinishAffinity();
            }
            else
            {
                var i = new Intent(Application.Context, typeof(LoginActivity));
                StartActivity(i);
                FinishAffinity();
            }

            base.OnStart();
        }

        private bool IsStillLoggedIn()
        {
            if (!TokenManager.IsExist())
            {
                return false;
            }
            if (TokenManager.IsExpired())
            {
                TokenManager.GenerateRefreshToken();
            }
            var token = TokenManager.GetActiveToken();
            if (token != null)
            {
                return true;
            }
            return false;
        }
    }
}