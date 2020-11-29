using MongoBDCore.Interfaces;
using MongoBDCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoBDCore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _database;
        public BookService(IBookRepository database)
        {
            _database = database;
        }
        public List<Book> GetAll() => _database.GetAll();
        public void AddRange(IEnumerable<Book> books)
        {
            books.ToList().ForEach(_database.Append);
        }
        public void Add(Book book)
        {
            _database.Append(book);
        }
        public int FindCountBooksMoreThen(int countMoreThen, Func<Book, string> orederBy, int numberBooksInResult)
        {
            var queryBooks = _database.GetFilteredBooks(book=>book.Count>countMoreThen)
                .OrderBy(orederBy)
                .Take(numberBooksInResult);            
            return queryBooks.Aggregate(0, (sum, book) => sum + book.Count);
        }
        public Book FindBookWithMaxCount()
        {
            return _database.GetAll()
                            .OrderByDescending(b => b.Count)
                            .FirstOrDefault();
        }
        public List<string> GetAllAuthors()
        {
            return _database.GetProjection(b => !String.IsNullOrEmpty(b.Author), b => b.Author)
                            .Cast<string>()                            
                            .Distinct()
                            .ToList();
        }
        public IEnumerable<Book> GetBooksWithoutAuthor()
        {
            return _database.GetFilteredBooks(b => String.IsNullOrEmpty(b.Author));
        }
        public void IncrementCountOfBookOn(int count)
        {
            _database.IncrementCountOfBooks(count);
        }
        public void AddGengeToBookWith(Func<Book, bool> filter, string addedGenre)
        {
            _database.InsertGenge(filter, addedGenre);
        }
        public void DeleteBooks(Func<Book, bool> filter)
        {
            _database.DeleteMany(filter);
        }
    }
}