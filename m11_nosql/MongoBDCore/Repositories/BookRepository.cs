using MongoBDCore.Interfaces;
using MongoBDCore.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MongoBDCore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IAppContext _database;
        public BookRepository(IAppContext database)
        {
            _database = database;
        }

        public void Create(Book book)
        {
           _database.Books.InsertOne(book);           
        }

        public void Delete(string id)
        {
            _database.Books.DeleteOne<Book>(b => b.Id.Equals(id));
        }

        public List<Book> GetAll()
        {
            return _database.Books.Find<Book>(_ => true).ToList();
        }

        public Book GetBook(string id)
        {
            return _database.Books.Find<Book>(b => b.Id.Equals(id)).FirstOrDefault();            
        }

        public void Update(string id, Book book)
        {
            _database.Books.ReplaceOne<Book>(b => b.Id.Equals(id), book);
        }
    }
}
