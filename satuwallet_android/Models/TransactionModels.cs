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

        public DateTime CreatedDateUtc { get; set; }

        public string Description1 { get; set; }

        public string Description2 { get; set; }


        public ActionTypes ActionType { get; set; }
        
        public Platform FromPlatform { get; set; }

        public UserRecord MeUser { get; set; }

        public Platform ToPlatform { get; set; }
        
        public UserRecord OtherUser { get; set; }

        public decimal Ammount { get; set; }

    }
}