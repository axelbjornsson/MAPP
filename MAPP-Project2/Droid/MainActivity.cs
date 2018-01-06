using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views.InputMethods;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;
using System.Collections.Generic;
using static Android.Support.V4.View.ViewPager;

namespace MovieApp.Droid
{
    [Activity(Label = "MainActivity", Theme = "@style/MyTheme")]
    public class MainActivity : FragmentActivity
    {
		public static MovieApi api;
  
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var fragments = new Fragment[]
            {
                new MovieSearchFragment(api),
                new TopRatedFragment(api)
            };

            var titles = CharSequence.ArrayFromStringArray(new[] { "Search", "Top Rated" });

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "Movie Search";

            viewPager.PageSelected += (sender, args) => {
                // If not in Search, don't use keyboard anymore
                if (args.Position != 0) 
                {
                    MovieSearchFragment fragment = (MovieSearchFragment)fragments[0];
                    fragment.HideKeyboard();
                }

                if (args.Position == 1)
                {
                    TopRatedFragment topRatedFragment = (TopRatedFragment)fragments[args.Position];
                    topRatedFragment.GetTopRatedAsync();
                }
            };
        }
    }
}

