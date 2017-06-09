using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var movie = new Movie
            {
                MovieId = 1,
                Rating = 1.2f,
                Movies = new List<Movie>
                {
                    new Movie
                    {
                        MovieId = 2,
                        Rating = 3.6f,
                        Movies = new List<Movie>
                        {
                            new Movie
                            {
                                MovieId = 4,
                                Rating = 4.8f
                            }
                        }
                    },
                    new Movie
                    {
                        MovieId = 3,
                        Rating = 2.4f,
                        Movies = new List<Movie>
                        {
                            new Movie
                            {
                                MovieId = 4,
                                Rating = 4.8f
                            }
                        }
                    },
                }
            };

            var n = 10;

            GetRecomendedMovies(movie, n);

            if (n > flatenMovies.Count)
            {
                PrintMovies(flatenMovies, flatenMovies.Count);
            }
            else
            {
                SortMovies(flatenMovies);
                PrintMovies(flatenMovies, n);
            }
        }

        private static void PrintMovies(List<Movie> movies, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(movies[movies.Count - (i + 1)].MovieId);
            }
        }

        private static void SortMovies(List<Movie> movies)
        {
            movies.Sort((movie, movie1) =>
            {
                if (movie1.Rating > movie.Rating) return -1;
                else if (movie1.Rating < movie.Rating) return 1;
                return 0;
            });
        }

        static Dictionary<int, int> visitedMovies = new Dictionary<int, int>();
        static List<Movie> flatenMovies = new List<Movie>();

        private static void GetRecomendedMovies(Movie movie, int n)
        {
            if (movie.Movies != null)
                foreach (var child in movie.Movies)
                {
                    if (!visitedMovies.ContainsKey(child.MovieId))
                    {
                        visitedMovies.Add(child.MovieId, 1);
                        flatenMovies.Add(new Movie
                        {
                            Rating = child.Rating,
                            MovieId = child.MovieId
                        });
                    }
                    GetRecomendedMovies(child, n);
                }

        }
    }

    public class Movie
    {
        public int MovieId { get; set; }
        public float Rating { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
