using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using satuwallet_android.Constants;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace satuwallet_android.Activities
{
    [Activity(Label = "Pay Result", Theme = "@style/AppTheme")]
    public class PayResultActivity : AppCompatActivity
    {
        private V7Toolbar mToolbar;

        private string mToBarcode;
        private Platform mPlatform;

        private Dictionary<Platform, string> mPlatformsDict;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_payresult);

            mToolbar = FindViewById<V7Toolbar>(Resource.Id.payresult_toolbar);
            SetSupportActionBar(mToolbar);

            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            mToBarcode = Intent.GetStringExtra("barcode");
            mPlatform = (Platform)Intent.GetIntExtra("platform", 0);

            var tvMobileNo = FindViewById<TextView>(Resource.Id.payresult_tvToMobileNo);
            tvMobileNo.Text = mToBarcode;

            var tvPlatform = FindViewById<TextView>(Resource.Id.payresult_tvPlatform);
            tvPlatform.Text = mPlatform.ToString();

            mPlatformsDict = PlatformExt.GetKeyValue();

            var spPlatform = FindViewById<AppCompatSpinner>(Resource.Id.payresult_spPlatform);

            string[] spinnerArray = mPlatformsDict.Select(x => x.Value).ToArray();
            ArrayAdapter<String> adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, spinnerArray);
            adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            spPlatform.SetAdapter(adapter);
            
            var btnPay = FindViewById(Resource.Id.payresult_btnPay);
            btnPay.Click += BtnPay_Click;
        }

        private void BtnPay_Click(object sender, EventArgs e)
        {
            // Send to server for payment
            // From other platform is possible
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}