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

namespace satuwallet_android.Interfaces
{
    public interface IResponseAction
    {
        void OnOk(string rawData);

        void OnBadRequest(string errorMessage);

        void OnUnauthorized(string errorMessage);

        void OnError(HttpResponseMessage responseMessage);

        void OnLocalError(Exception e);
    }
}