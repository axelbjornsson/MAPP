using MovieApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFMovieSearch.Views;

namespace XFMovieSearch.ViewModels
{
    public class ListViewModel : INotifyPropertyChanged
    {
        protected INavigation navigation;
        protected MovieApi api;

        protected MovieInformation selectedMovie;
        protected List<MovieInformation> movieList;
        protected bool loading = false;

        public void GetCastsAndDetails()
        {
            api.GetCasts(MovieList);
            api.GetDetails(MovieList);
        }


        public MovieInformation SelectedMovie
        {
            get => selectedMovie;
            set
            {
                if (value != null)
                {
                    this.selectedMovie = value;
                    this.navigation.PushAsync(new MovieDetailPage(this.selectedMovie), true);
                }
            }
        }

        public bool Loading
        {
            get => loading;
            set
            {
                loading = value;
                OnPropertyChanged();
            }
        }

        public List<MovieInformation> MovieList
        {
            get => movieList;
            set
            {
                this.movieList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
