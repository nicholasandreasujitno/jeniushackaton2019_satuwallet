using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using satuwallet_android.Fragments;
using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;

namespace satuwallet_android.Adapters
{
    class MainPagerAdapter : Android.Support.V4.App.FragmentPagerAdapter
    {
        List<V4Fragment> fragments = new List<V4Fragment>();
        List<string> fragmentTitles = new List<string>();
        List<int> fragmentIconResIds = new List<int>();

        public MainPagerAdapter(V4FragmentManager fm) : base(fm) { }

        public void AddFragment(V4Fragment fragment, string title, int iconResIds)
        {
            fragments.Add(fragment);
            fragmentTitles.Add(title);
            fragmentIconResIds.Add(iconResIds);
        }
        public override V4Fragment GetItem(int position)
        {
            return fragments[position];
        }
        public override int Count
        {
            get
            {
                return fragments.Count;
            }
        }        
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(fragmentTitles[position]);
        }
        public int GetIconResId(int position)
        {
            return fragmentIconResIds[position];
        }

        //public override int GetItemPosition(Java.Lang.Object objectValue)
        //{
        //    return PositionNone;
        //}

        public V4Fragment GetFragment(int position)
        {
            return fragments[position];
        }

        public override void SetPrimaryItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            if (fragments[position].GetType() == typeof(HomeFragment))
            {
                var fr = (HomeFragment)fragments[position];
                fr.GenerateChildren();
            }
            else if (fragments[position].GetType() == typeof(EasyRegisterFragment))
            {
                var fr = (EasyRegisterFragment)fragments[position];
                fr.GenerateChildren();
            }

            base.SetPrimaryItem(container, position, @object);
        }        
    }
}