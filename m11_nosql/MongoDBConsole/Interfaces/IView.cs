using MongoBDCore.Models;
using System.Collections.Generic;

namespace MongoDBConsole.Interfaces
{
    public interface IView
    {
        void Show(Book book);
        void Show(IEnumerable<Book> books);
    }
}