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

namespace MovieApp.Droid
{
    [Activity(Label = "MovieListActivity", Theme = "@style/MyTheme")]
    public class MovieListActivity : Activity 
    {
        private List<MovieDTO> movieList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SearchList);

            this.movieList = new List<MovieDTO>();
            var jsonStr = this.Intent.GetStringExtra("movieList");
            this.movieList = JsonConvert.DeserializeObject<List<MovieDTO>>(jsonStr);

            var listView = this.FindViewById<ListView>(Resource.Id.listView);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "Movie Search";

            listView.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this, typeof(MovieDetailActivity));
                intent.PutExtra("movie", JsonConvert.SerializeObject(movieList[args.Position]));
                this.StartActivity(intent);
            };

            listView.Adapter = new MovieListAdapter(this, this.movieList);
        }
    }
}
