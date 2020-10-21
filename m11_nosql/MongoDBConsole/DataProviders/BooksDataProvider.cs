using MongoBDCore.Models;
using MongoDBConsole.Interfaces;
using System.Collections.Generic;

namespace MongoDBConsole.DataProviders
{
    public class BooksDataProvider : IDataProvider
    {
        public IEnumerable<Book> GetBooks()
        {
            return new List<Book>(new[]
            {
                new Book()
                {
                    Name="Hobbit",
                    Author="Tolkien",
                    Genres=new List<string>(new []
                    {
                        "fantasy"
                    }),
                    Count=5,
                    Year=2014,

                },
                new Book()
                {
                    Name="Repka",
                    Genres=new List<string>(new []
                    {
                        "kids"
                    }),
                    Count=11,
                    Year=2000
                },
                new Book()
                {
                    Name="Lord of the rings",
                    Author="Tolkien",
                    Genres=new List<string>(new []
                    {
                        "fantasy"
                    }),
                    Count=3,
                    Year=2015
                },
                new Book()
                {
                    Name="Kolobok",
                    Genres=new List<string>(new []
                    {
                        "kids"
                    }),
                    Count=10,
                    Year=2000
                },
                new Book()
                {
                    Name="Dyadya Stiopa",
                    Author="Mihalkov",
                    Genres=new List<string>(new []
                    {
                        "kids"
                    }),
                    Count=1,
                    Year=2001
                }
            });
        }
    }
}
