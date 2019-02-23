using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using satuwallet_android.Models;
using ZXing;
using ZXing.Common;
using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace satuwallet_android.Activities
{
    [Activity(Label = "My QR", Theme = "@style/AppTheme")]
    public class MyQRActivity : AppCompatActivity
    {
        private V7Toolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //SupportActionBar  
            toolbar = FindViewById<V7Toolbar>(Resource.Id.main_toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 600,
                    Width = 600
                }
            };
            var user = DbContext.GetConnection().Table<User>().FirstOrDefault();
            var bitmap = writer.Write(user.Username);

            var ivQR = FindViewById<ImageView>(Resource.Id.myqr_ivQR);
            ivQR.SetImageBitmap(bitmap);
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