using System;
using System.Collections.Generic;
using DM.MovieApi.MovieDb.Movies;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieApp
{
    public class MovieInformation : INotifyPropertyChanged
    {
        private int id;
        private string title;
        private string releaseYear;
        private string cast;
        private string posterPath;
        private string backdropPath;
        private string overview;
        private string genres;
        private string runtime;
        private string tagline;

        private const string imagePath = "http://image.tmdb.org/t/p/original";

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string ReleaseYear { get => releaseYear; set => releaseYear = value; }
        public string Cast
        {
            get => cast;
            set
            {
                if (value != null)
                {
                    this.cast = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PosterPath { get => posterPath; set => posterPath = imagePath + value; }
        public string BackdropPath { get => backdropPath; set => backdropPath = imagePath + value; }
        public string Overview { get => overview; set => overview = value; }
        public string Genres { get => genres; set => genres = value; }

        public string Runtime
        {
            get => runtime;
            set
            {
                if (value != null)
                {
                    this.runtime = value + " min";
                    OnPropertyChanged();
                }
            }
        }

        public string Tagline
        {
            get => tagline;
            set
            {
                if (value != null)
                {
                    this.tagline = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TitleAndYear { get => Title + " (" + ReleaseYear + ")"; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
