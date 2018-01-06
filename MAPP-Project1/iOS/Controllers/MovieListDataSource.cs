using System;
using System.Collections.Generic;
using Foundation;
using MovieApp.iOS.Views;
using UIKit;

namespace MovieApp.iOS.Controllers
{
    public class MovieListDataSource : UITableViewSource
    {
        private readonly List<MovieDTO> movieList;
        private readonly Action<int> OnSelectedMovie;

        private readonly NSString movieListCellId = new NSString("MovieListCell");

        public MovieListDataSource(List<MovieDTO> movieList, Action<int> OnSelectedMovie)
        {
            this.movieList = movieList;
            this.OnSelectedMovie = OnSelectedMovie;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.RowHeight = 100;
            var cell = (MovieTableViewCell)tableView.DequeueReusableCell((NSString)this.movieListCellId);

            if (cell == null)
            {
                cell = new MovieTableViewCell(this.movieListCellId);
            }

            cell.UpdateCell(this.movieList[indexPath.Row]);

            return cell;
        }

        public override System.nint RowsInSection(UITableView tableview, System.nint section)
        {
            return this.movieList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            this.OnSelectedMovie(indexPath.Row);
        }
    }
}