﻿using MovieApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XFMovieSearch.Models;
using XFMovieSearch.Views;

namespace XFMovieSearch
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var api = new MovieApi();

            var searchPage = new NavigationPage(new SearchPage(api))
            {
                Title = "Search"
            };

            var topRatedPage = new NavigationPage(new OrderByPage(api, Order.TopRated))
            {
                Title = "Top Rated"
            };

            var popularPage = new NavigationPage(new OrderByPage(api, Order.Popular))
            {
                Title = "Popular"
            };

            var tabbedPage = new TabbedPage(); ;
            tabbedPage.Children.Add(searchPage);
            tabbedPage.Children.Add(topRatedPage);
            tabbedPage.Children.Add(popularPage);
            
            MainPage = tabbedPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
