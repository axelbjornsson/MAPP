using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace MovieApp
{
    public class MovieApi
    {
        private IApiMovieRequest api;

        public MovieApi()
        {
            MovieDbFactory.RegisterSettings(new ApiSettings());
            api = MovieDbFactory.Create<IApiMovieRequest>().Value;
        }

        public async Task<string> GetMovieTitleAsync(string searchQuery)
        {
            string title = "";

            try
            {
                var response = await api.SearchByTitleAsync(searchQuery);
                title = response.Results.FirstOrDefault().Title;
            } catch
            {
                title = "Error";
            }

            return title;
        }

        public async Task<List<MovieInformation>> GetSearchedMoviesAsync(string searchQuery)
        {
            try
            {
                var response = await api.SearchByTitleAsync(searchQuery);
                return GetListFromResponse(response);
            }
            catch {
                return await GetSearchedMoviesAsync(searchQuery);
            }
        }

        public async Task<List<MovieInformation>> GetTopRatedMoviesAsync()
        {
            var response = await api.GetTopRatedAsync();

            return GetListFromResponse(response);
        }

        public async Task<List<MovieInformation>> GetPopularMoviesAsync()
        {
            var response = await api.GetPopularAsync();

            return GetListFromResponse(response);
        }

        public void GetDetails(List<MovieInformation> movieList)
        {
            foreach (var movie in movieList)
            {
                GetMovieDetailAsync(movie);
            }
        }

        private async void GetMovieDetailAsync(MovieInformation movie)
        {
            try
            {
                var movieDetails = await api.FindByIdAsync(movie.Id);

                movie.Runtime = movieDetails.Item.Runtime.ToString();
                movie.Tagline = movieDetails.Item.Tagline;
            }
            catch
            {
                GetMovieDetailAsync(movie);
            }
        }

        public void GetCasts(List<MovieInformation> movieList)
        {
            foreach (var movie in movieList)
            {
                GetCastAsync(movie);
            }
        }

        private async void GetCastAsync(MovieInformation movie)
        {
            try
            {
                var castList = new List<string>();
                var credits = await api.GetCreditsAsync(movie.Id);
                var castMembers = credits.Item.CastMembers;
                foreach (var x in castMembers.Take(3))
                    castList.Add(x.Name);
                movie.Cast = ListToString(castList);
            }
            catch
            {
                GetCastAsync(movie);
            }
        }

        private List<MovieInformation> GetListFromResponse(ApiSearchResponse<MovieInfo> response)
        {
            List<MovieInformation> movies = new List<MovieInformation>();

            foreach (MovieInfo movieInfo in response.Results)
            {
                MovieInformation movie = GetMovieFromMovieInfo(movieInfo);

                movies.Add(movie);
            }

            return movies;
        }

        private MovieInformation GetMovieFromMovieInfo(MovieInfo movieInfo)
        {
            var movie = new MovieInformation
            {
                Id = movieInfo.Id,
                Title = movieInfo.Title,
                ReleaseYear = movieInfo.ReleaseDate.Year.ToString(),
                PosterPath = movieInfo.PosterPath,
                BackdropPath = movieInfo.BackdropPath,
                Overview = movieInfo.Overview,
                Genres = ListToString(GetGenres(movieInfo)),
            };

            return movie;
        }

        private static List<string> GetGenres(MovieInfo movieInfo)
        {
            var genres = new List<string>();
            foreach (var genre in movieInfo.Genres)
                genres.Add(genre.Name);

            return genres;
        }

        private string ListToString(List<string> list)
        {
            string fancyString = "";
            foreach (var x in list)
            {
                if (fancyString != string.Empty)
                    fancyString += ", ";
                if (x != null)
                    fancyString += x;
            }
            return fancyString;
        }
    }
}
