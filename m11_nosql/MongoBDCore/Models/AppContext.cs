using Microsoft.Extensions.Options;
using MongoBDCore.Interfaces;
using MongoBDCore.Models.Configuration;
using MongoDB.Driver;
using MongoDBCore.Constants;

namespace MongoBDCore.Models
{
    public class AppContext : IAppContext
    {
        private readonly IMongoDatabase _database;
        public AppContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client?.GetDatabase(settings.Value.DatabaseName);
        }
        public IMongoCollection<Book> Books => _database.GetCollection<Book>(AppContextConstants.BOOK_COLLECTION_NAME);
    }
}
