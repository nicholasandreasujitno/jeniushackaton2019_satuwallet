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
    public class Transaction
    {
        public int Id { get; set; }
        public Platform Platform { get; set; }
        public string Description { get; set; }
        public decimal Ammount { get; set; }

    }
}