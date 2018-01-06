using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFMovieSearch.ViewModels;

namespace XFMovieSearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailPage : ContentPage
    {
        private MovieDetailViewModel movieDetailViewModel;

        public MovieDetailPage(MovieApp.MovieInformation selectedMovie)
        {
            movieDetailViewModel = new MovieDetailViewModel(this.Navigation, selectedMovie);
            this.BindingContext = movieDetailViewModel;

            InitializeComponent();
        }


    }
}