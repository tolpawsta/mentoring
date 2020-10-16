using MongoBDCore.Models;
using MongoDB.Driver;

namespace MongoBDCore.Interfaces
{
    public interface IAppContext
    {
        IMongoCollection<Book> Books { get; }
    }
}