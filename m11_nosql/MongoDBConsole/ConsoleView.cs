using MongoBDCore.Models;
using MongoDBConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBConsole
{
    public class ConsoleView : IView
    {
        public void Show(IEnumerable<Book> books)
        {
            if (books?.Count() > 0)
            {
                foreach (var book in books)
                {
                    Show(book);
                }
            }
            else
            {
                Console.WriteLine("Books not found");
            }

        }
        public void Show(Book book)
        {
            Console.WriteLine("Book: " +
                    $"\n Id is {book.Id}" +
                    $"\n Name is {book.Name}" +
                    $"\n Author is {book.Author ?? "\'No author added\'"}" +
                    $"\n Count is {book.Count}" +
                    $"\n Year is {book.Year}" +
                    $"\n Genres: ");
            book.Genres.ToList().ForEach(genre => Console.WriteLine($"\t{genre}"));
        }
    }
}
