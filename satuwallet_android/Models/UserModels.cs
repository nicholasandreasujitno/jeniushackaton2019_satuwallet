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
using SQLite;

namespace satuwallet_android.Models
{
    public class UserRegisterBinding
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }

    public class User
    {
        [PrimaryKey]
        public long Id { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }
}