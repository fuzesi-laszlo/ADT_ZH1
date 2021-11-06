using System;
using System.Linq;
using System.Xml.Linq;

namespace ZH.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Load Xml content to DB.
            MovieDbContext dbContext = new();
            var movies = XDocument.Load("Movies.xml").GetMoviesFromXml();
            foreach (Movie movie in movies)
            {
                foreach (Actor actor in movie.Actors)
                {
                    dbContext.Add(actor);
                }
                dbContext.Add(movie);
            }
            dbContext.SaveChanges();

            /*
             * dbContext.Movies.PrintToConsole("All Movies");
             * dbContext.Actors.PrintToConsole("All Actors");
             */

            // a,
            Console.WriteLine("Total number of actors: " + dbContext.Actors.Count() + "\n");

            // b,
            dbContext.Actors.Where(actor => actor.Sex == "Male").PrintToConsole("Male Actors");

            // c,
            int maxYear = dbContext.Movies.Max(movie => movie.YearOfRelease);
            Console.Write("The most recent movie is: ");
            Console.WriteLine(dbContext.Movies.Single(movie => movie.YearOfRelease == maxYear));

            // d,
            var queryD = dbContext.Actors
                .Where(actor => actor.Sex == "Female")
                .Join(dbContext.Movies,
                      femaleActor => femaleActor.MovieId,
                      movie => movie.Id,
                      (female, movie) => movie)
                .Distinct();

            queryD.Where(movie => movie.YearOfRelease == queryD.Min(movie => movie.YearOfRelease))
                .PrintToConsole("Oldest Movie(s) with Female Actor");


            // e,
            queryD.OrderBy(movie => movie.YearOfRelease).PrintToConsole("Movies with Female Actors ordered by creation date ascending");

            dbContext.Dispose();
            //movies.ToList().ForEach(movie => movie.Actors.PrintToConsole(movie.MyToString()));
        }
    }
}
