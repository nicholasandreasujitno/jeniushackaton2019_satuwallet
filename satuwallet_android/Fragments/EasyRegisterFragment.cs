﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using satuwallet_android.Constants;
using V4Fragment = Android.Support.V4.App.Fragment;
using V7GridLayout = Android.Support.V7.Widget.GridLayout;

namespace satuwallet_android.Fragments
{
    public class EasyRegisterFragment : V4Fragment
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

            View view = inflater.Inflate(Resource.Layout.frag_easyregister, container, false);

            var vwPlatformContainer = view.FindViewById<V7GridLayout>(Resource.Id.easyregister_vwPlatformContainer);

            var totalPlatform = (Enum.GetNames(typeof(Platform))).Length;

            //var i = 0;
            foreach (var p in Enum.GetValues(typeof(Platform)))
            {
                View vwChild = inflater.Inflate(Resource.Layout.base_platform, null);
                
                vwPlatformContainer.AddView(vwChild);
                //i++;
            }

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}