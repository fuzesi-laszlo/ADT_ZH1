using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ZH.App
{
    public static class MyExtensions
    {
        public static void PrintToConsole<T>(this IEnumerable<T> input, string title = "")
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($" *** BEGIN: {title} ");
            Console.ResetColor();

            foreach (T item in input)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" ___ END OF {title} ___ (Press a key to continue...)");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static string MyToString<T>(this T stringableType)
        {
            System.Text.StringBuilder sb = new();
            var nonvirtualProperties = typeof(T).GetProperties()
                .Where(propertyInfo => !propertyInfo.GetGetMethod().IsVirtual);

            foreach (var propertyInfo in nonvirtualProperties)
            {
                sb.Append($"{propertyInfo.Name} = {propertyInfo.GetValue(stringableType)}, ");
            }

            return sb.ToString();
        }

        public static IEnumerable<Movie> GetMoviesFromXml(this XDocument xDoc)
        {
            Movie currentMovie;
            List<Actor> actorList;

            foreach (var movieElement in xDoc.Root.Descendants("Movie"))
            {
                actorList = new();

                currentMovie = new()
                {
                    Title = movieElement.Element("Title").Value,
                    Genre = movieElement.Element("Genre").Value,
                    Rating = movieElement.Element("Rating").Value,
                    YearOfRelease = int.Parse(movieElement.Element("YearOfRelease").Value),
                };

                foreach (var actorElement in movieElement.Descendants("Actor"))
                {
                    actorList.Add(new Actor
                    {
                        Name = actorElement.Element("Name").Value,
                        Sex = actorElement.Element("Sex").Value,
                        MovieId = currentMovie.Id,
                        Movie = currentMovie,
                    });
                }

                currentMovie.Actors = actorList;

                yield return currentMovie;
            }
        }
    }
}
