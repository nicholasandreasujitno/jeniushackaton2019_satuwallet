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
using satuwallet_android.Constants;

namespace satuwallet_android.Models
{
    public class PlatformRegistrationBinding
    {
        public Platform Platform { get; set; }
    }

        public class PlatformRegistration
    {
        public long Id { get; set; }

        public Platform Platform { get; set; }

        public decimal Balance { get; set; }
    }
}