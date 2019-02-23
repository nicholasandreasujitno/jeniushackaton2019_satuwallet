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
using satuwallet_android.Adapters;
using satuwallet_android.Helpers;
using satuwallet_android.Models;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace satuwallet_android.Activities
{
    [Activity(Label = "History", Theme = "@style/AppTheme")]
    public class HistoryActivity : AppCompatActivity
    {
        private V7Toolbar mToolbar;

        private ListViewCompat mLvTransaction;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_history);

            mToolbar = FindViewById<V7Toolbar>(Resource.Id.history_toolbar);
            SetSupportActionBar(mToolbar);

            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            mLvTransaction = FindViewById<ListViewCompat>(Resource.Id.history_lvTransaction);

            List<Transaction> transactions = new List<Transaction>();
            for (int i = 0; i < 20; i++)
            {
                decimal amm = ((i + (new RandomGenerator(DateTime.Now)).RandomInt(3)) * 100);
                transactions.Add(new Transaction()
                {
                    Platform = Constants.Platform.OVO,
                    Description = "" + i,
                    Ammount = amm
                });
            }

            var adapter = new HistoryTransactionAdapter(this, transactions);
            mLvTransaction.Adapter = adapter;
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