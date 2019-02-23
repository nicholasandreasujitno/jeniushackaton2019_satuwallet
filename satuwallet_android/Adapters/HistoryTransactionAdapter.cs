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
using Java.Lang;
using satuwallet_android.Constants;
using satuwallet_android.Models;

namespace satuwallet_android.Adapters
{
    public class ViewHolder : Java.Lang.Object
    {
        public string PlatformName { get; set; }
        public string Description { get; set; }
        public decimal Ammount { get; set; }
    }

    public class HistoryTransactionAdapter : BaseAdapter
    {
        public AppCompatActivity activity;
        public List<Transaction> transactions;

        public HistoryTransactionAdapter (AppCompatActivity activity, List<Transaction> transactions)
        {
            this.activity = activity;
            this.transactions = transactions;
        }

        public override int Count
        {
            get { return transactions.Count(); }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null; //transactions[position];
        }

        public override long GetItemId(int position)
        {
            return (int)transactions[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.base_history_list_row, parent, false);

            var tvDescription1 = view.FindViewById<TextView>(Resource.Id.basehistoryrow_tvDescription1);
            var tvDescription2 = view.FindViewById<TextView>(Resource.Id.basehistoryrow_tvDescription2);
            var tvAmmount = view.FindViewById<TextView>(Resource.Id.basehistoryrow_tvAmmount);

            tvDescription1.Text = transactions[position].FromPlatform.ToString();
            tvDescription2.Text = transactions[position].Description2 + "";
            tvAmmount.Text = (transactions[position].Ammount >= 0 ? "+" : "") + string.Format("{0:n0}", transactions[position].Ammount);

            return view;
        }
    }
}