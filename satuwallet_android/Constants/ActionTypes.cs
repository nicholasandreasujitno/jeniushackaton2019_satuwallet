﻿using System;
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
    public enum ActionTypes
    {
        Pembayaran = 0,
        Transfer = 4,
        Penerimaan = 7,

        Withdraw = 21,
        Deposit = 27
    }
}