using MovieApp;
using System;
using MovieApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFMovieSearch.ViewModels;

namespace XFMovieSearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderByPage : ContentPage
    {
        OrderByListViewModel viewModel;
        public OrderByPage(MovieApi api, Models.Order order)
        {
            viewModel = new OrderByListViewModel(Navigation, api, order);
            this.BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
