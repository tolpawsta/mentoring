using MongoBDCore.Interfaces;
using MongoBDCore.Models;
using MongoDB.Driver;
using System;
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
        public void Append(Book book)
        {
           _database.Books.InsertOne(book);
        }      
        public List<Book> GetAll()
        {
            return _database.Books.Find<Book>(_ => true).ToList();
        }
        public Book GetBook(string id)
        {
            return _database.Books.Find<Book>(b => b.Id.Equals(id)).FirstOrDefault();            
        }
        public List<Book> GetFilteredBooks(Func<Book,bool> filter)
        {
            var filteredBooks = Builders<Book>.Filter.Where(book => filter(book));            
            return _database.Books.Find<Book>(filteredBooks).ToList();
        }
        public List<object> GetProjection(Func<Book,bool> filter,Func<Book,object> projection)
        {
            var bookProjections = Builders<Book>.Projection.Include(book => projection(book));
            return _database.Books.Find<Book>(b=>filter(b)).Project<object>(bookProjections).ToList();
        }
        public void Update(string id, Book book)
        {
            _database.Books.ReplaceOne<Book>(b => b.Id.Equals(id), book);
        }
        public void IncrementCountOfBooks(int count)
        {
            var updateIncCount = new UpdateDefinitionBuilder<Book>().Inc(b => b.Count, count);
           _database.Books.UpdateMany<Book>(_ => true, updateIncCount);            
        }
        public void InsertGenge(Func<Book,bool> filter,string addedGenre)
        {
            var updateGenres = Builders<Book>.Update.AddToSet("Genre", addedGenre);            
           _database.Books.UpdateMany<Book>(book=>filter(book) & !book.Genres.Contains(addedGenre), updateGenres);
        }
        public List<Book> GetBooksWithCountLessThen(int count)
        {
            var filterBuilder = Builders<Book>.Filter.Lt("Count", count);            
            return _database.Books.Find<Book>(filterBuilder).ToList();
        }
        public void Delete(string id)
        {
            _database.Books.DeleteOne<Book>(b => b.Id.Equals(id));
        }
        public void DeleteMany(Func<Book,bool> filter)
        {
            _database.Books.DeleteMany(book => filter(book));
        }
    }
}
