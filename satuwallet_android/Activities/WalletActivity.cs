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
    [Activity(Label = "Wallets", Theme = "@style/AppTheme")]
    public class WalletActivity : AppCompatActivity
    {
        private V7Toolbar mToolbar;
        private Dictionary<Platform, string> mPlatformsDict;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_wallet);

            mToolbar = FindViewById<V7Toolbar>(Resource.Id.wallet_toolbar);
            SetSupportActionBar(mToolbar);

            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            mPlatformsDict = PlatformExt.GetKeyValue();

            var spFromPlatform = FindViewById<AppCompatSpinner>(Resource.Id.wallet_spFromPlatform);
            var spToPlatform = FindViewById<AppCompatSpinner>(Resource.Id.wallet_spToPlatform);

            string[] spinnerArray = mPlatformsDict.Select(x => x.Value).ToArray();
            ArrayAdapter<String> adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, spinnerArray);
            adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            spFromPlatform.SetAdapter(adapter);
            spToPlatform.SetAdapter(adapter);
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