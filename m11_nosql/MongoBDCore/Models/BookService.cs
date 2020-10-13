using MongoBDCore.Interfaces.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoBDCore.Models
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        public BookService(IAppDbConfiguration configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            var database = client.GetDatabase(configuration.DatabaseName);
            _books = database.GetCollection<Book>(typeof(Book).Name);
        }
        public List<Book> GetAll()
        {
            return _books.Find(b => true).ToList();
        }

        public Book GetBook(string id)
        {
            return _books.Find<Book>(b => b.Id == id).FirstOrDefault();
        }
        public void Create(Book book)
        {
            _books.InsertOne(book);
        }
        public void Update(string id, Book book)
        {
            _books.ReplaceOne<Book>(b => b.Id == id, book);
        }
        public void Delete(string id)
        {
            _books.DeleteOne<Book>(b => b.Id == id);
        }
    }
}