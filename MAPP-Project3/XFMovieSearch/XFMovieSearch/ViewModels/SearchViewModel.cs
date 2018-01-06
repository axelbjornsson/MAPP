using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp;
using Xamarin.Forms;
using System.Windows.Input;
using XFMovieSearch.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XFMovieSearch.ViewModels
{
    public class SearchViewModel : ListViewModel
    {
        public string searchQuery;

        public SearchViewModel(INavigation navigation, MovieApi api)
        {
            this.navigation = navigation;
            this.api = api;
        }

        public ICommand SearchCommand
        {
            get {
                return new Command(async () =>
                {
                    if (searchQuery != "" && searchQuery != "")
                    {
                        Loading = true;
                        MovieList = new List<MovieInformation>();
                        MovieList = await api.GetSearchedMoviesAsync(searchQuery);
                        Loading = false;
                        GetCastsAndDetails();
                    }
                });
            }
        }

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                if (value != null)
                {
                    searchQuery = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
