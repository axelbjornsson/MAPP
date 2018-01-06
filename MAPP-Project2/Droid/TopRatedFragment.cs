
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;

namespace MovieApp.Droid
{
    public class TopRatedFragment : Fragment
    {
        private MovieApi api;
        private List<MovieDTO> movieList;
        private ListView listView;
        private ProgressBar progressBar;
        private View rootView;
        private bool searching = false;

        public TopRatedFragment(MovieApi api)
        {
            this.api = api;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			base.OnCreateView(inflater, container, savedInstanceState);

            movieList = new List<MovieDTO>();

            rootView = inflater.Inflate(Resource.Layout.MovieList, container, false);

            listView = rootView.FindViewById<ListView>(Resource.Id.listView);
            progressBar = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar);

            listView.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this.Context, typeof(MovieDetailActivity));
                intent.PutExtra("movie", JsonConvert.SerializeObject(movieList[args.Position]));
                this.StartActivity(intent);
            };

            return rootView;
        }

        public async void GetTopRatedAsync() 
        {
            if (!searching)
            {
				// Update UI Before
				Activity.RunOnUiThread(() => {
					movieList = new List<MovieDTO>();
					listView.Adapter = new MovieListAdapter(this.Activity, movieList);
					progressBar.Visibility = ViewStates.Visible;
				});
				
				searching = true;
				movieList = await api.GetTopRatedMoviesAsync();
				searching = false;
				
				// Update UI After
				Activity.RunOnUiThread(() => {
					listView.Adapter = new MovieListAdapter(this.Activity, movieList);
					progressBar.Visibility = ViewStates.Gone;            
				});
			}
        }
    }
}
