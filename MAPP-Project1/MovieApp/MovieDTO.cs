using System;
using System.Collections.Generic;
using DM.MovieApi.MovieDb.Movies;

namespace MovieApp
{
    public class MovieDTO
    {
        public readonly string title;
        public readonly string releaseYear;
        public readonly string cast;
        public string imagePath;
        public readonly string overview;
        public readonly string genres;
        public readonly string runtime;

        public MovieDTO(string title, string releaseYear, string cast, string posterPath, string overview, string genres, string runtime)
        {
            this.title = title;
            this.releaseYear = releaseYear;
            this.cast = cast;
            this.imagePath = posterPath;
            this.overview = overview;
            this.genres = genres;
            this.runtime = runtime;
        }
    }
}
