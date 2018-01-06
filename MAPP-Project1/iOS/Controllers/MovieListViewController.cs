using System.Collections.Generic;
using UIKit;

namespace MovieApp.iOS.Controllers
{
    public class MovieListViewController : UITableViewController
    {
        private readonly List<MovieDTO> movieList;

        public MovieListViewController(List<MovieDTO> movieList)
        {
            this.movieList = movieList;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Movie List";

            this.TableView.Source = new MovieListDataSource(this.movieList, OnSelectedMovie); 
        }

        private void OnSelectedMovie(int row) 
        {
            this.NavigationController.PushViewController(new MovieDetailsViewController(movieList[row]), true);
        }
    }
}