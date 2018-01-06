
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.InputMethodServices;
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
    [Activity(Label = "Movie Search", Theme = "@style/MyTheme")]
    public class MovieSearchFragment : Fragment
    {
        private readonly MovieApi api;
        private EditText searchInput;

        public MovieSearchFragment(MovieApi api)
        {
            this.api = api;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var rootView = inflater.Inflate(Resource.Layout.MovieSearch, container, false);

            searchInput = rootView.FindViewById<EditText>(Resource.Id.titleEditText);
            var searchButton = rootView.FindViewById<Button>(Resource.Id.searchButton);
            var progressBar = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.Visibility = ViewStates.Invisible;

            searchButton.Click += async (object sender, EventArgs e) =>
            {
                HideKeyboard();

                List<MovieDTO> movieList = new List<MovieDTO>();

                if (searchInput.Text.Length > 0)
                {
                    searchButton.Enabled = false;
                    progressBar.Visibility = ViewStates.Visible;
                    movieList = await api.GetMoviesAsync(searchInput.Text);
                    progressBar.Visibility = ViewStates.Invisible;
                    searchButton.Enabled = true;
                }

                var intent = new Intent(this.Context, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(movieList));
                this.StartActivity(intent);

            };


            return rootView;
        }

        public void HideKeyboard()
        {
            var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
            manager.HideSoftInputFromWindow(searchInput.WindowToken, 0);
        }
    }
}
