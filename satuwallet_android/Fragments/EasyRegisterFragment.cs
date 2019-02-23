using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using satuwallet_android.Constants;
using satuwallet_android.Helpers;
using satuwallet_android.Interfaces;
using satuwallet_android.Models;
using V4Fragment = Android.Support.V4.App.Fragment;
using V7GridLayout = Android.Support.V7.Widget.GridLayout;

namespace satuwallet_android.Fragments
{
    public class EasyRegisterFragment : V4Fragment
    {
        private static View mView;
        private static LayoutInflater mInflater;
        //private static V4Fragment mFragment;
        //private static LayoutInflater mInflater;
        //private static AppCompatActivity mActivity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            mView = inflater.Inflate(Resource.Layout.frag_easyregister, container, false);

            mInflater = inflater;

            //mFragment = this;
            //mInflater = inflater;
            //var totalPlatform = (Enum.GetNames(typeof(Platform))).Length;

            //var i = 0;
            //foreach (var p in Enum.GetValues(typeof(Platform)))
            //{
            //    View vwChild = inflater.Inflate(Resource.Layout.base_platform, null);

            //    vwChild.Click += VwChild_Click;

            //    vwPlatformContainer.AddView(vwChild);
            //    //i++;
            //}
            GenerateChildren();

            return mView;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public void GenerateChildren()
        {
            var vwPlatformContainer = mView.FindViewById<V7GridLayout>(Resource.Id.easyregister_vwPlatformContainer);
            vwPlatformContainer.RemoveAllViews();

            var dataPRs = DbContext.GetConnection().Table<PlatformRegistration>();

            foreach (Platform p in Enum.GetValues(typeof(Platform)))
            {
                if (dataPRs != null)
                {
                    var activePlatforms = dataPRs.Select(x => x.Platform);
                    if (activePlatforms.Contains(p))
                    {
                        continue;
                    }
                }
                View vwChild = mInflater.Inflate(Resource.Layout.base_platform, null);
                var vwIcon = vwChild.FindViewById<ImageView>(Resource.Id.baseplatform_icon);
                vwIcon.SetImageResource(p.GetLogoResId());

                var vwTitle = vwChild.FindViewById<TextView>(Resource.Id.baseplatform_title);
                vwTitle.Text = "" + p.ToString();

                var vwPId = vwChild.FindViewById<TextView>(Resource.Id.baseplatform_id);
                vwPId.Text = "" + (int)p;

                var vwBtn = vwChild.FindViewById(Resource.Id.baseplatform_vwBtn);
                vwBtn.Click += VwChild_Click;

                vwPlatformContainer.AddView(vwChild);
            }
        }

        private void VwChild_Click(object sender, EventArgs e)
        {
            var view = (View)sender;

            var vwPId = view.FindViewById<TextView>(Resource.Id.baseplatform_id);
            var platform = (Platform)int.Parse(vwPId.Text);

            var restService = new RestService(true);
            var pb = new PlatformRegistrationBinding()
            {
                Platform = platform,
            };
            var data = restService.ConvertToJson(pb);
            var pr = restService.Post<PlatformRegistration>(ApiUrl.Platform_Create, data);
            DbContext.GetConnection().InsertOrReplace(pr);

            GenerateChildren();
        }
        //public class ResponseFetchActivePlatform : IResponseAction
        //{
        //    public void OnBadRequest(string errorMessage)
        //    {
        //    }

        //    public void OnError(HttpResponseMessage responseMessage)
        //    {
        //    }

        //    public void OnLocalError(Exception e)
        //    {
        //    }

        //    public void OnOk(string rawData)
        //    {
        //        mFragment.Activity.RunOnUiThread(() =>
        //        {
        //            var inflater = mFragment.Activity.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
        //            var vwPlatformContainer = mView.FindViewById<V7GridLayout>(Resource.Id.easyregister_vwPlatformContainer);

        //            var dataPRs = JsonConvert.DeserializeObject<List<PlatformRegistration>>(rawData);

        //            foreach (Platform p in Enum.GetValues(typeof(Platform)))
        //            {
        //                if (dataPRs != null)
        //                {
        //                    var activePlatforms = dataPRs.Select(x => x.Platform);
        //                    if (activePlatforms.Contains(p))
        //                    {
        //                        continue;
        //                    }
        //                }
        //                View vwChild = inflater.Inflate(Resource.Layout.base_platform, null);
        //                var vwIcon = vwChild.FindViewById<ImageView>(Resource.Id.baseplatform_icon);
        //                vwIcon.SetImageResource(p.GetLogoResId());

        //                var vwTitle = vwChild.FindViewById<TextView>(Resource.Id.baseplatform_title);
        //                vwTitle.Text = "" + p.ToString();

        //                var vwPId = vwChild.FindViewById<TextView>(Resource.Id.baseplatform_id);
        //                vwPId.Text = "" + (int) p;

        //                vwChild.Click += VwChild_Click;

        //                vwPlatformContainer.AddView(vwChild);
        //            }
        //        });

        //    }

        //    public void OnUnauthorized(string errorMessage)
        //    {
        //    }

        //    private void VwChild_Click(object sender, EventArgs e)
        //    {
        //        RestService restService = new RestService(true);
        //        var httpResponseMessage = restService.Post(ApiUrl.Platform_Create, "");
        //        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
        //        {

        //        }
        //        else
        //        {
        //            mFragment.Activity.TryShowDialog("Error Fast Registration", "Error " + httpResponseMessage.StatusCode + ".");
        //        }
        //    }
        //}

    }
}