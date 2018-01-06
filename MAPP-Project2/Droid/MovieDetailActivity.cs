
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
using Com.Bumptech.Glide;
using Newtonsoft.Json;

namespace MovieApp.Droid
{
    [Activity(Label = "MovieDetailActivity", Theme = "@style/MyTheme")]
    public class MovieDetailActivity : Activity
    {
        private const string ImageLink = "http://image.tmdb.org/t/p/original";

        private MovieDTO movie;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MovieDetail);

            var jsonStr = this.Intent.GetStringExtra("movie");
            this.movie = JsonConvert.DeserializeObject<MovieDTO>(jsonStr);

            var splashImage = this.FindViewById<ImageView>(Resource.Id.splashImage);
            var movieTitle  = this.FindViewById<TextView>(Resource.Id.movieTitle);
			var genres      = this.FindViewById<TextView>(Resource.Id.genres);
            var posterImage = this.FindViewById<ImageView>(Resource.Id.posterImage);
            var description = this.FindViewById<TextView>(Resource.Id.description);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "Movie Details";

            Glide.With(this).Load(ImageLink + movie.splashImagePath).Into(splashImage);
			movieTitle.Text = movie.title;
            genres.Text = movie.runtime + "m | " + movie.genres;
            Glide.With(this).Load(ImageLink + movie.imagePath).Into(posterImage);
            description.Text = movie.overview;


        }
    }
}
