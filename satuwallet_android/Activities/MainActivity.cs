using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using satuwallet_android.Adapters;

using System.Collections.Generic;
using Android.Support.Design.Widget;
using satuwallet_android.Fragments;
using Android.Views;
using Android.Graphics;
using Android.Support.V4.Content.Res;
using Android.Support.V7.Widget;
using satuwallet_android.Helpers;
using Android.Content;
using System;
using Android.Support.V4.Content;

using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
//using ActionBar = Android.App.ActionBar;
//using Fragment = Android.Support.V4.App.Fragment;
//using FragmentManager = Android.Support.V4.App.FragmentManager;
//using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
//using satuwallet_android.Fragments;

namespace satuwallet_android.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, TabLayout.IOnTabSelectedListener
    {
        private static AppCompatActivity thisPage;
        private V7Toolbar toolbar;
        //private ActionBar ab;
        private ViewPager viewpager;
        //private MenuPagerAdapter adapter;
        private V4FragmentManager fragmentManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            thisPage = this;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //SupportActionBar  
            toolbar = FindViewById<V7Toolbar>(Resource.Id.main_toolbar);
            SetSupportActionBar(toolbar);

            // Set Toolbar Title Font
            for (int i = 0; i < toolbar.ChildCount; i++)
            {
                View view = toolbar.GetChildAt(i);
                //var a = view.GetType();
                // AppCompatTextView instead of TextView
                if (view.GetType() == typeof(AppCompatTextView))
                {
                    AppCompatTextView tv = (AppCompatTextView)view;
                    Typeface titleFont = ResourcesCompat.GetFont(this, Resource.Font.magnolia_script);
                    if (tv.Text.Equals(toolbar.Title))
                    {
                        tv.SetTypeface(titleFont, TypefaceStyle.Normal);
                        break;
                    }
                }
            }

            //Typeface customFont = Typeface.CreateFromAsset(this.Assets, "fonts/Calibri.ttf");
            //SupportActionBar.setTypeface(customFont);

            //SupportActionBar.SetIcon(Resource.Drawable.ic_dashboard_black_24dp);

            fragmentManager = this.SupportFragmentManager;

            //ViewPager  
            viewpager = FindViewById<ViewPager>(Resource.Id.main_view_pager);
            SetupViewPager(viewpager); //Calling SetupViewPager Method  
                                       //TabLayout  
            var tabLayout = FindViewById<TabLayout>(Resource.Id.main_tab_layout);
            tabLayout.SetupWithViewPager(viewpager);
            tabLayout.AddOnTabSelectedListener(this);

            SetupTabs((MainPagerAdapter)viewpager.Adapter, tabLayout);
            //FloatingActionButton  
            //var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += (sender, e) => {
            //    Snackbar.Make(fab, "Here's a snackbar!", Snackbar.LengthLong).SetAction("Action", v => Console.WriteLine("Action handler")).Show();
            //};

            //ab = this.ActionBar;
            //ab.NavigationMode = ActionBarNavigationMode.Tabs;

            //fm = this.SupportFragmentManager;

            //adapter = new MenuPagerAdapter(fm, GetPages());

            //vp = FindViewById<ViewPager>(Resource.Id.view_pager);
            //vp.PageSelected += Vp_PageSelected;
            //vp.Adapter = adapter;

            //AddTabs();
        }

        void SetupViewPager(ViewPager viewPager1)
        {
            var adapter = new Adapters.MainPagerAdapter(fragmentManager);
            adapter.AddFragment(new HomeFragment(), "Home", Resource.Drawable.ic_home_white_24dp);
            adapter.AddFragment(new EasyRegisterFragment(), "Platform", Resource.Drawable.ic_baseline_add_24dp);
            adapter.AddFragment(new UserFragment(), "User", Resource.Drawable.ic_baseline_face_24px);
            viewPager1.Adapter = adapter;
            viewPager1.Adapter.NotifyDataSetChanged();
        }

        void SetupTabs(MainPagerAdapter adapter, TabLayout tabLayout)
        {
            //var adapter = (MainPagerAdapter)viewpager.Adapter;

            var isActive = true;
            for (var i = 0; i < adapter.Count; i++)
            {
                View view1 = LayoutInflater.Inflate(Resource.Layout.base_tab, null);
                view1.FindViewById(Resource.Id.basetab_icon).SetBackgroundResource(adapter.GetIconResId(i));
                view1.FindViewById<TextView>(Resource.Id.basetab_title).Text = adapter.GetPageTitleFormatted(i).ToString();
                ChangeTabBaseStyle(view1, isActive);
                tabLayout.GetTabAt(i).SetCustomView(view1);
                isActive = false;
            }
            
            //View view2 = LayoutInflater.Inflate(Resource.Layout.tab_base, null);
            //view2.FindViewById(Resource.Id.basetab_icon).SetBackgroundResource(adapter.GetIconResId(1));
            //view2.FindViewById<TextView>(Resource.Id.basetab_title).Text = adapter.GetPageTitleFormatted(1).ToString();
            //ChangeTabBaseStyle(view2, false);
            //tabLayout.GetTabAt(1).SetCustomView(view2);

            //ViewGroup.LayoutParams view1Params = view2.LayoutParameters;

            //ViewGroup.LayoutParams params1 = tabLayout.LayoutParameters;
            //params1.Height = view1Params.Height;
            //tabLayout.LayoutParameters = params1;

            //var t = tabLayout.GetTabAt(1);
            //tabLayout.GetTabAt(1).SetIcon(Resource.Drawable.ic_home_white_24dp);
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_logout:
                    TokenManager.ClearToken();

                    var i = new Intent(Application.Context, typeof(LoginActivity));
                    StartActivity(i);
                    FinishAffinity();
                    break;
            }

            //Toast.MakeText(this, item.TitleFormatted, ToastLength.Short);
            return base.OnOptionsItemSelected(item);
        }

        public void OnTabReselected(TabLayout.Tab tab)
        {
            Toast.MakeText(this, "OnTabReselected" + tab.Text, ToastLength.Short).Show();
        }

        public void OnTabSelected(TabLayout.Tab tab)
        {

            ChangeTabBaseStyle(tab.CustomView, true);

            //Toast.MakeText(this, "OnTabSelected" + tab.Text, ToastLength.Short).Show();
        }

        public void OnTabUnselected(TabLayout.Tab tab)
        {
            ChangeTabBaseStyle(tab.CustomView, false);

            Toast.MakeText(this, "OnTabUnselected" + tab.Text, ToastLength.Short).Show();
        }

        void ChangeTabBaseStyle(View tabCustomview, bool isActive)
        {
            if (isActive)
            {
                ChangeTabBaseStyle(tabCustomview, Resource.Color.white_text);
            }
            else
            {
                ChangeTabBaseStyle(tabCustomview, Resource.Color.disabled_tab);
            }
        }
        void ChangeTabBaseStyle(View tabCustomview, int colorResId)
        {
            var color1 = new Android.Graphics.Color(ContextCompat.GetColor(this, colorResId));

            var tabIcon1 = tabCustomview.FindViewById<ImageView>(Resource.Id.basetab_icon);
            var srcImg = tabIcon1.Background;
            var newBg = DrawableCompatMore.GetTintedDrawableOfColorResId(this, srcImg, colorResId);
            tabIcon1.Background = newBg;

            tabCustomview.FindViewById<TextView>(Resource.Id.basetab_title).SetTextColor(color1);
        }

        //private void Vp_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        //{
        //    ab.SetSelectedNavigationItem(e.Position);
        //}

        //private JavaList<Fragment> GetPages()
        //{
        //    var pages = new JavaList<Fragment>();
        //    pages.Add(new Test1Fragment());
        //    pages.Add(new Test2Fragment());
        //    return pages;
        //}

        //private void AddTabs()
        //{
        //    ActionBar.Tab t1 = ab.NewTab().SetText("test1").SetTabListener(this);
        //    ActionBar.Tab t2 = ab.NewTab().SetText("test2").SetTabListener(this);
        //    ActionBar.Tab t3 = ab.NewTab().SetText("test3").SetTabListener(this);

        //    ab.AddTab(t1);
        //    ab.AddTab(t2);
        //    ab.AddTab(t3);
        //}

        //public void OnTabReselected(ActionBar.Tab tab, Android.App.FragmentTransaction ft)
        //{
        //    //throw new System.NotImplementedException();
        //}

        //public void OnTabSelected(ActionBar.Tab tab, Android.App.FragmentTransaction ft)
        //{
        //    //throw new System.NotImplementedException();
        //}

        //public void OnTabUnselected(ActionBar.Tab tab, Android.App.FragmentTransaction ft)
        //{
        //    //throw new System.NotImplementedException();
        //}

        //private MenuPagerAdapter adapter;
        //private CustomViewPager pager;
        //void InitAdapter()
        //{
        //    string[] titles = new[]
        //                 {
        //                    "Home",
        //                    "..."
        //                };
        //    pager = FindViewById<CustomViewPager>(Resource.Id.pager);
        //    pager.EnableTouchEvents(true);
        //    adapter = new MenuPagerAdapter(SupportFragmentManager, titles);
        //    var pageMargin = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Resources.DisplayMetrics);
        //    pager.OffscreenPageLimit = titles.Length;//amount of fragments you want to stay in memory(for faster tab change) 
        //    pager.PageMargin = pageMargin;
        //    pager.AddOnPageChangeListener(this);
        //    pager.Adapter = adapter;
        //    pager.CurrentItem = 0;
        //}

    }
}