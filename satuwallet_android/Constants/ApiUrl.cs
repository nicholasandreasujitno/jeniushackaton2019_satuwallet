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

namespace satuwallet_android.Constants
{
    public class ApiUrl
    {
        public const string Base = "http://satuwallet.be-hire.com";

        public const string Account_Register = Base + "/api/Account/Register";
        public const string Account_Detail = Base + "/api/Account/Detail";

        public const string Token = Base + "/token";
    }
}