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
using satuwallet_android.Constants;
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

            var RandomGenerator = new RandomGenerator(DateTime.Now);
            var rnd = RandomGenerator.GetRandom();

            List<Transaction> transactions = new List<Transaction>();
            for (int i = 0; i < 15; i++)
            {
                var fromP = GetPlatformByRandom(rnd);
                var toP = GetPlatformByRandom(rnd);

                var msg = "";
                if (fromP != toP)
                {
                    msg += toP.ToString();
                }
                var actT = GetATByRandom(rnd);

                decimal amm = ((i + (new RandomGenerator(DateTime.Now)).RandomInt(3)) * 100);
                transactions.Add(new Transaction()
                {
                    FromPlatform = Constants.Platform.OVO,
                    Description1 = fromP.ToString(),
                    Description2 = actT.ToString() + " " + msg,
                    Ammount = (actT == ActionTypes.Penerimaan) ? amm : -amm,
                });
            }

            var adapter = new HistoryTransactionAdapter(this, transactions);
            mLvTransaction.Adapter = adapter;
        }
        
        private Platform GetPlatformByRandom(Random rnd)
        {
            var i = rnd.Next(1, 5); // creates a number between 1 and 4
            switch (i)
            {
                case 1:
                    return Platform.OVO;
                    break;
                case 2:
                    return Platform.GoPay;
                    break;
                case 3:
                    return Platform.DANA;
                    break;
                default:
                    return Platform.iSaku;
                    break;
            }
        }


        private ActionTypes GetATByRandom(Random rnd)
        {
            var i = rnd.Next(1, 4); // creates a number between 1 and 4
            switch (i)
            {
                case 1:
                    return ActionTypes.Pembayaran;
                    break;
                case 2:
                    return ActionTypes.Penerimaan;
                    break;
                case 3:
                    return ActionTypes.Transfer;
                    break;
                default:
                    return ActionTypes.Pembayaran;
                    break;
            }
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