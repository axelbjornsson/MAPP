using System;
using DM.MovieApi;

namespace MovieApp
{
    public class ApiSettings : IMovieDbSettings
    {
        string IMovieDbSettings.ApiKey => "5793cbda74d29720fa7558ead89910bb";
        string IMovieDbSettings.ApiUrl => "http://api.themoviedb.org/3/";
    }
}
