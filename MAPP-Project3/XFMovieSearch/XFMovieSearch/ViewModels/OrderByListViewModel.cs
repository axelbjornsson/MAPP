using MovieApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFMovieSearch.Models;

namespace XFMovieSearch.ViewModels
{
    public class OrderByListViewModel : ListViewModel
    {
        Order order;
    
        public ICommand RefreshCommand {
            get {
                return new Command(async () =>
                {
                    Loading = true;
                    await GetListAsync();
                    Loading = false;
                });
            }
        }

        public string OrderTitle
        {
            get
            {
                if (order == Order.TopRated)
                    return "Top Rated";
                else return "Popular";
            }
        }

        public OrderByListViewModel(INavigation navigation, MovieApi api, Order order)
        {
            this.navigation = navigation;
            this.api = api;

            this.order = order;

            GetListAsync();
        }

        public async Task GetListAsync()
        {
            try
            {
                if (order == Order.TopRated)
                    await GetTopRatedMovies();
                else if (order == Order.Popular)
                    await GetPopularMovies();

                GetCastsAndDetails();
            } catch { }
        }


        private async Task GetTopRatedMovies()
        {
            try
            {
                this.MovieList = await api.GetTopRatedMoviesAsync();
            }
            catch
            {
                await GetTopRatedMovies();
            }
        }

        private async Task GetPopularMovies()
        {
            try
            {
                this.MovieList = await api.GetPopularMoviesAsync();
            }
            catch 
            {
                await GetPopularMovies();
            }
        }
    }
}
