using MongoBDCore.Models;
using System.Collections.Generic;

namespace MongoDBConsole.Interfaces
{
    public interface IDataProvider
    {
        IEnumerable<Book> GetBooks();
    }
}