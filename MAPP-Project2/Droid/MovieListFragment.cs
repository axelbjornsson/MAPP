
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
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;

namespace MovieApp.Droid
{
    // ATH EKKI NOTAÐ
    [Activity(Label = "MovieListFragment", Theme = "@style/MyTheme")]
    public class MovieListFragment : Fragment 
    {
        private List<MovieDTO> movieList;
        private ListView listView;

        public MovieListFragment() {}

        public MovieListFragment(List<MovieDTO> movieList) {
            this.movieList = movieList;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.MovieList, container, false);

            listView = rootView.FindViewById<ListView>(Resource.Id.listView);

            return rootView;
        }
    }
}
