using System;
using System.Collections.Generic;
using CoreGraphics;
using MovieDownload;
using UIKit;

namespace MovieApp.iOS.Controllers
{
    public class MovieSearchViewController : UIViewController
    {
        private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;
        private MovieApi movieApi;
        private List<MovieDTO> movieList;
        private ImageDownloader imageDownloader;

        public MovieSearchViewController(MovieApi movieApi, ImageDownloader imageDownloader)
        {
            this.movieApi = movieApi;
            this.imageDownloader = imageDownloader;

            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Movie Search";
            this.View.BackgroundColor = iOS.Stylesheet.backgroundColor;

            UILabel promptLabel = PromptLabel();
            UITextField nameField = NameField();
            UIActivityIndicatorView activityIndicator = ActivityIndicator();

            UIButton searchButton = SearchButton(nameField, activityIndicator);

            this.View.AddSubviews(new UIView[] {
                promptLabel,
                nameField,
                searchButton,
                activityIndicator
            });

            this.View.BringSubviewToFront(activityIndicator);
        }

        private UIActivityIndicatorView ActivityIndicator()
        {
            return new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray)
            {
                Frame = new CGRect(StartX, StartY * 4, this.View.Bounds.Width - 2 * StartX, Height),
                HidesWhenStopped = true
            };
        }

        private UIButton SearchButton(UITextField nameField, UIActivityIndicatorView activityIndicator)
        {
            var searchButton = UIButton.FromType(UIButtonType.RoundedRect);
            searchButton.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            searchButton.SetTitle("Get movie", UIControlState.Normal);
            searchButton.TouchUpInside += async (sender, args) =>
            {
                nameField.ResignFirstResponder();
                searchButton.Enabled = false;
                movieList = new List<MovieDTO>();
                if (nameField.Text.Length > 0)
                {
					activityIndicator.StartAnimating();
                    movieList = await movieApi.GetMoviesAsync(nameField.Text);
                    await imageDownloader.DownloadImagesFromListOfMovieDTOs(movieList);
                    activityIndicator.StopAnimating();
                }
                this.NavigationController.PushViewController(new MovieListViewController(movieList), true);
				searchButton.Enabled = true;
            };

            return searchButton;
        }

        private UITextField NameField()
        {
            return new UITextField()
            {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };
        }

        private UILabel PromptLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter words in movie title:"
            };
        }
    }
}