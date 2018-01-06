using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using MovieDownload;
using UIKit;

namespace MovieApp.iOS.Controllers
{
    public class MovieRatingsViewController : UITableViewController
    {
        private MovieApi movieApi;
        private ImageDownloader imageDownloader;
        private List<MovieDTO> movieList;
        private LoadingOverlay loadingOverlay;

        private bool reload = true;

        public MovieRatingsViewController(MovieApi movieApi, ImageDownloader imageDownloader)
        {


            this.movieApi = movieApi;
            this.imageDownloader = imageDownloader;

            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 0);
        }

        public override void ViewDidLoad()
        {
			base.ViewDidLoad();

            this.Title = "Top Rated";
        }


        public override async void ViewDidAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (reload)
            {
                await UpdateListAsync();
            }

            reload = true;
        }

		private async System.Threading.Tasks.Task UpdateListAsync()
		{
			// Reset Top Rated
			movieList = new List<MovieDTO>();
			
			this.reload = true;
			
			// When loading starts
			LoadingStarted();
			
			// Get data
			this.movieList = await movieApi.GetTopRatedMoviesAsync();
			await imageDownloader.DownloadImagesFromListOfMovieDTOs(movieList);
			
			LoadingFinished();
		}

        private void LoadingStarted()
        {
            this.View.UserInteractionEnabled = false;
			loadingOverlay = new LoadingOverlay(UIScreen.MainScreen.Bounds);
			this.View.Add(loadingOverlay);
		}

        private void LoadingFinished()
        {
			// When loading stops
            loadingOverlay.Hide();
            this.View.UserInteractionEnabled = true;
			
            // Set TableView after data
			this.TableView.Source = new MovieListDataSource(this.movieList, OnSelectedMovie);
			this.TableView.ReloadData();
        }

        private void OnSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieDetailsViewController(movieList[row]), true);
            this.reload = false;
        }
    }
}
