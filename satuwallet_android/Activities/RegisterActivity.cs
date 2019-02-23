using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace satuwallet_android.Activities
{
    [Activity(Label = "Register", Theme = "@style/LoginTheme")]
    public class RegisterActivity : AppCompatActivity
    {
        public static AppCompatActivity thisPage;
        private V7Toolbar mToolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            thisPage = this;

            // Create your application here
            SetContentView(Resource.Layout.activity_register);

            mToolbar = FindViewById<V7Toolbar>(Resource.Id.register_toolbar);
            SetSupportActionBar(mToolbar);

            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            
            var btnRegister = FindViewById<Button>(Resource.Id.register_btnRegister);
            btnRegister.Click += BtnRegister_Click;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }
            return base.OnOptionsItemSelected(item);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var isValid = true;
            var invalidMsg = new List<string>();

            var etMobileNo = FindViewById<EditText>(Resource.Id.register_etUsername);
            var etPassword = FindViewById<EditText>(Resource.Id.register_etPassword);
            var etFirstname = FindViewById<EditText>(Resource.Id.register_etFirstname);
            var etLastname = FindViewById<EditText>(Resource.Id.register_etLastname);
            var etEmail = FindViewById<EditText>(Resource.Id.register_etEmail);
            if (string.IsNullOrEmpty(etMobileNo.Text))
            {
                invalidMsg.Add("Mobile no");
                isValid = false;
            }
            if (string.IsNullOrEmpty(etPassword.Text))
            {
                invalidMsg.Add("Password");
                isValid = false;
            }
            if (string.IsNullOrEmpty(etFirstname.Text))
            {
                invalidMsg.Add("First name");
                isValid = false;
            }
            if (string.IsNullOrEmpty(etEmail.Text))
            {
                invalidMsg.Add("Email");
                isValid = false;
            }

            if (!isValid)
            {
                var strMsg = string.Join(", ", invalidMsg);
                //await DisplayAlert("Register Failed", $"Please fill in {strMsg}.", "Ok");
                this.ShowDialog("Register Failed", $"Please fill in {strMsg}.", "Ok");
                return;
            }

            var userBinding = new UserRegisterBinding()
            {
                Username = etMobileNo.Text,
                Password = etPassword.Text,
                MobileNo = etMobileNo.Text,
                Firstname = etFirstname.Text,
                Lastname = etLastname.Text,
                Email = etEmail.Text,
            };

            var restService = new RestService();

            var data = restService.ConvertToJson(userBinding);
            var httpResponse = restService.Post(ApiUrl.Account_Register, data);
            //restService.ProcessResponse(httpResponse, new RegisterResponse());
        }
        
    }
}