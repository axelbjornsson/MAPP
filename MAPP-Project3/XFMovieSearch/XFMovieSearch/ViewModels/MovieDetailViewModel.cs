using MovieApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFMovieSearch.ViewModels
{
    class MovieDetailViewModel
    {
        private INavigation navigation;

        public MovieInformation Movie { get; set; }

        public MovieDetailViewModel(INavigation navigation, MovieInformation selectedMovie)
        {
            this.navigation = navigation;
            Movie = selectedMovie;
        }


    }
}
