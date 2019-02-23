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
using satuwallet_android.Models;
using V4Fragment = Android.Support.V4.App.Fragment;
using V7GridLayout = Android.Support.V7.Widget.GridLayout;

namespace satuwallet_android.Fragments
{
    public class HomeFragment : V4Fragment
    {
        private View mView;
        private LayoutInflater mInflater;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            mView = inflater.Inflate(Resource.Layout.frag_home, container, false);

            mInflater = inflater;

            //var vwPlatformContainer = mView.FindViewById<V7GridLayout>(Resource.Id.home_vwPlatformContainer);

            //var totalPlatform = (Enum.GetNames(typeof(Platform))).Length;
            ////var i = 0;
            //foreach (var p in Enum.GetValues(typeof(Platform)))
            //{
            //    View vwChild = inflater.Inflate(Resource.Layout.base_platform, null);

            //    vwPlatformContainer.AddView(vwChild);
            //    //i++;
            //}

            GenerateChildren();

            var vwPay = mView.FindViewById(Resource.Id.home_vwPay);
            vwPay.Click += VwPay_Click;
            var vwWallet = mView.FindViewById(Resource.Id.home_vwWallet);
            vwWallet.Click += VwWallet_Click; ;
            var vwHistory = mView.FindViewById(Resource.Id.home_vwHistory);
            vwHistory.Click += VwHistory_Click;
            
            return mView;
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

        public void GenerateChildren()
        {
            if (mView == null)
            {
                return;
            }
            var vwPlatformContainer = mView.FindViewById<V7GridLayout>(Resource.Id.home_vwPlatformContainer);
            vwPlatformContainer.RemoveAllViews();

            var dataPRs = DbContext.GetConnection().Table<PlatformRegistration>();

            foreach (Platform p in Enum.GetValues(typeof(Platform)))
            {
                if (dataPRs != null)
                {
                    var activePlatforms = dataPRs.Select(x => x.Platform);
                    if (!activePlatforms.Contains(p))
                    {
                        continue;
                    }
                }

                var existingPR = dataPRs.FirstOrDefault(x => x.Platform == p);

                View vwChild = mInflater.Inflate(Resource.Layout.base_platform, null);
                var vwIcon = vwChild.FindViewById<ImageView>(Resource.Id.baseplatform_icon);
                vwIcon.SetImageResource(p.GetLogoResId());

                var vwTitle = vwChild.FindViewById<TextView>(Resource.Id.baseplatform_title);
                vwTitle.Text = "" + p.ToString();

                var vwBalance = vwChild.FindViewById<TextView>(Resource.Id.baseplatform_balance);
                vwBalance.Text = string.Format("{0:n0}", existingPR.Balance);

                var vwPId = vwChild.FindViewById<TextView>(Resource.Id.baseplatform_id);
                vwPId.Text = "" + (int)p;

                //var vwBtn = vwChild.FindViewById(Resource.Id.baseplatform_vwBtn);
                //vwBtn.Click += VwChild_Click;

                vwPlatformContainer.AddView(vwChild);
            }
        }
    }
}