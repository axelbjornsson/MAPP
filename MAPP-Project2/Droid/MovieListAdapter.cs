using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;

namespace MovieApp.Droid
{
    public class MovieListAdapter : BaseAdapter<MovieDTO>
    {
        private Activity activity;
        private readonly List<MovieDTO> movieList;

        private const string ImageLink = "http://image.tmdb.org/t/p/original";

        public MovieListAdapter(Activity activity, List<MovieDTO> movieList)
        {
            this.activity = activity;
            this.movieList = movieList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView; // re--use an existing view, if one is available

            if (view == null)
                view = this.activity.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);

            var movie = this.movieList[position];

            var movieTitle = view.FindViewById<TextView>(Resource.Id.title);
            var moviePoster = view.FindViewById<ImageView>(Resource.Id.poster);
            var movieActors = view.FindViewById<TextView>(Resource.Id.actors);

            movieTitle.Text = movie.title + " (" + movie.releaseYear + ")";
            movieActors.Text = movie.cast;

            Glide.With(activity).Load(ImageLink + movie.imagePath).Into(moviePoster);
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count => this.movieList.Count;

        public override MovieDTO this[int position] => this.movieList[position];
    }
}