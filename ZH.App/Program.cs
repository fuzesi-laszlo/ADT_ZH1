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

            dbContext.Movies.PrintToConsole("All Movies");
            dbContext.Actors.PrintToConsole("All Actors");

            dbContext.Dispose();
            //movies.ToList().ForEach(movie => movie.Actors.PrintToConsole(movie.MyToString()));
        }
    }
}
