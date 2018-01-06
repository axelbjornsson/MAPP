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

        public async Task<List<MovieDTO>> GetMoviesAsync(string searchQuery)
        {
            var response = await api.SearchByTitleAsync(searchQuery);

            return await GetListFromResponse(response);
        }

		public async Task<List<MovieDTO>> GetTopRatedMoviesAsync()
		{
			var response = await api.GetTopRatedAsync();

            return await GetListFromResponse(response);
			
		}
  
        private async Task<List<MovieDTO>> GetListFromResponse(ApiSearchResponse<MovieInfo> response)
        {
            List<MovieDTO> movies = new List<MovieDTO>();

            foreach (MovieInfo movieInfo in response.Results)
            {
                MovieDTO movie = await GetMovieDTOFromMovieInfo(movieInfo);

                movies.Add(movie);
            }

            return movies;
        }

        private async Task<MovieDTO> GetMovieDTOFromMovieInfo(MovieInfo movieInfo)
        {
            var cast = await GetCastAsync(movieInfo.Id);
            var runtime = await GetRuntimeAsync(movieInfo.Id);

            var genres = GetGenres(movieInfo);

            var movie = new MovieDTO(movieInfo.Title,
                                     movieInfo.ReleaseDate.Year.ToString(),
                                     ListToString(cast),
                                     movieInfo.PosterPath,
                                     movieInfo.Overview,
                                     ListToString(genres),
                                     runtime);
            return movie;
        }

        private static List<string> GetGenres(MovieInfo movieInfo)
        {
            var genres = new List<string>();
            foreach (var genre in movieInfo.Genres)
                genres.Add(genre.Name);

            return genres;
        }

        private async Task<string> GetRuntimeAsync(int movieId)
        {
            try
            {
                var movie = await api.FindByIdAsync(movieId);
                var runtime = movie.Item.Runtime.ToString();
                return runtime;
            }
            catch
            {
                return await GetRuntimeAsync(movieId);
            }
        }
        private async Task<List<string>> GetCastAsync(int movieId)
        {
            try
            {
				var castList = new List<string>();
                var credits = await api.GetCreditsAsync(movieId);
                var castMembers = credits.Item.CastMembers;
				foreach (var x in castMembers.Take(3))
					castList.Add(x.Name);
				return castList;
            }
            catch {
                return await GetCastAsync(movieId);
            }

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
