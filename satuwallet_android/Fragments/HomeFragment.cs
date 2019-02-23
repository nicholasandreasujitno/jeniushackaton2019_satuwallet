using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using satuwallet_android.Activities;
using satuwallet_android.Constants;
using V4Fragment = Android.Support.V4.App.Fragment;
using V7GridLayout = Android.Support.V7.Widget.GridLayout;

namespace satuwallet_android.Fragments
{
    public class HomeFragment : V4Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.frag_home, container, false);


            var vwPlatformContainer = view.FindViewById<V7GridLayout>(Resource.Id.home_vwPlatformContainer);

            var totalPlatform = (Enum.GetNames(typeof(Platform))).Length;
            //var i = 0;
            foreach (var p in Enum.GetValues(typeof(Platform)))
            {
                View vwChild = inflater.Inflate(Resource.Layout.base_platform, null);

                vwPlatformContainer.AddView(vwChild);
                //i++;
            }

            var vwPay = view.FindViewById(Resource.Id.home_vwPay);
            vwPay.Click += VwPay_Click;
            var vwWallet = view.FindViewById(Resource.Id.home_vwWallet);
            vwWallet.Click += VwWallet_Click; ;
            var vwHistory = view.FindViewById(Resource.Id.home_vwHistory);
            vwHistory.Click += VwHistory_Click;


            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void VwPay_Click(object sender, EventArgs e)
        {
            var i = new Intent(Application.Context, typeof(PayActivity));
            StartActivity(i);
        }

        private void VwWallet_Click(object sender, EventArgs e)
        {
            var i = new Intent(Application.Context, typeof(WalletActivity));
            StartActivity(i);
        }

        private void VwHistory_Click(object sender, EventArgs e)
        {
            var i = new Intent(Application.Context, typeof(HistoryActivity));
            StartActivity(i);
        }
    }
}