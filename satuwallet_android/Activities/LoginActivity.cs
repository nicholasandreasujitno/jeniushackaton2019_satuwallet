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
using satuwallet_android.Constants;
using satuwallet_android.Helpers;
using satuwallet_android.Models;

namespace satuwallet_android.Activities
{
    [Activity(Label = "LoginActivity", Theme = "@style/LoginTheme")]
    public class LoginActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_login);

            var tvGoToRegister = FindViewById<TextView>(Resource.Id.login_tvGoToRegister);
            tvGoToRegister.Click += TvGoToRegister_Click;


            var btnLogin = FindViewById<Button>(Resource.Id.login_btnLogin);
            btnLogin.Click += BtnLogin_Click;
        }
        
        private void TvGoToRegister_Click(object sender, EventArgs e)
        {
            var i = new Intent(Application.Context, typeof(RegisterActivity));
            StartActivity(i);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            var invalidMsg = new List<string>();

            var etUsername = FindViewById<EditText>(Resource.Id.login_etUsername);
            var etPassword = FindViewById<EditText>(Resource.Id.login_etPassword);
            if (string.IsNullOrEmpty(etUsername.Text))
            {
                invalidMsg.Add("Username");
                isValid = false;
            }
            if (string.IsNullOrEmpty(etPassword.Text))
            {
                invalidMsg.Add("Password");
                isValid = false;
            }
            if (!isValid)
            {
                var strMsg = string.Join(", ", invalidMsg);
                this.ShowDialog("Login Failed", $"Please fill in {strMsg}.", "Ok");
                return;
            }

            var token = TokenManager.GenerateAccessToken(etUsername.Text, etPassword.Text);

            if (token == null)
            {
                this.ShowDialog("Login Failed", "Username and password is invalid", "Ok");
                return;
            }

            var restService = new RestService(true);

            var user = restService.Post<User>(ApiUrl.Account_Detail, "");
            if (user == null)
            {
                this.ShowDialog("Login Failed", "Error. User not found.", "Ok");
                return;
            }
            
            var database = DbContext.GetConnection();
            database.BeginTransaction();
            try
            {
                database.Execute("DELETE FROM User");
                database.InsertOrReplace(user);
                database.Commit();
            }
            catch (Exception ex)
            {
                database.Rollback();
                throw ex;
            }
            
            var i = new Intent(Application.Context, typeof(MainActivity));
            StartActivity(i);
            FinishAffinity();

            //Application.Current.MainPage = new NavigationPage(new HomePage());
        }
    }
}