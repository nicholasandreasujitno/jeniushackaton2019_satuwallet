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
using V7AlertDialog = Android.Support.V7.App.AlertDialog;

namespace satuwallet_android.Helpers
{
    public static class DialogHelper
    {
        public static V7AlertDialog TryShowDialog(this Context context, string title, string body,
            string positiveText = null, string negativeText = null, Action<bool> clickedAction = null)
        {
            if (context.GetType() == typeof(AppCompatActivity))
            {
                var activity = (AppCompatActivity)context;
                return activity.ShowDialog("Post Error", "Cannot read response.", "Ok");
            }
            return null;
        }

        public static V7AlertDialog ShowDialog(this AppCompatActivity activity, string title, string body,
        string positiveText = null, string negativeText = null, Action<bool> clickedAction = null)
        {
            V7AlertDialog adDialog = null;

            activity.RunOnUiThread(() =>
            {
                //V7AlertDialog.Builder bd = new V7AlertDialog.Builder(activity, Resource.Style.AlertDialog_AppCompat_Light);
                V7AlertDialog.Builder bd = new V7AlertDialog.Builder(activity, Resource.Style.MyAlertDialog);
                bd.SetTitle(title);
                bd.SetMessage(body);
                if (!string.IsNullOrEmpty(positiveText))
                {
                    bd.SetPositiveButton(positiveText, new EventHandler<DialogClickEventArgs>(
                    (s, args) =>
                    {
                        activity.RunOnUiThread(() =>
                        {
                            adDialog.Hide();
                            adDialog.Cancel();
                        });
                        if (clickedAction != null)
                        {
                            clickedAction(true);
                        }
                    }));
                }
                if (!string.IsNullOrEmpty(negativeText))
                {
                    bd.SetNegativeButton(negativeText, new EventHandler<DialogClickEventArgs>(
                        (s, args) =>
                        {
                            activity.RunOnUiThread(() =>
                            {
                                adDialog.Hide();
                                adDialog.Cancel();
                            });
                            if (clickedAction != null)
                            {
                                clickedAction(false);
                            }
                        }));
                }

                adDialog = bd.Create();
                //adDialog.SetCanceledOnTouchOutside(true);
                adDialog.Show();
                //adDialog = new AlertDialog.Builder(activity).Create();

                //View dialogView;
                //dialogView = LayoutInflater.From(activity).Inflate(Resource.Layout.DialogBodyLayout, null);

                //var tdb = dialogView.FindViewById<TextView>(Resource.Id.textDialogBody);
                //tdb.Text = body;

                //adDialog.SetTitle(title);
                //adDialog.SetView(dialogView);
                //adDialog.SetButton(positiveText, new EventHandler<DialogClickEventArgs>(
                //    (s, args) => {
                //        activity.RunOnUiThread(() => {
                //            adDialog.Hide();
                //            adDialog.Cancel();
                //        });
                //        clicked(true);
                //    }));
                //adDialog.SetButton2(negativeText, new EventHandler<DialogClickEventArgs>(
                //    (s, args) => {
                //        activity.RunOnUiThread(() => {
                //            adDialog.Hide();
                //            adDialog.Cancel();
                //        });
                //        clicked(false);
                //    }));

                //adDialog.Show();
            });

            return adDialog;
        }
    }
}