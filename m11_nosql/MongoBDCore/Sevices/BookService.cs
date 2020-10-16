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
            books.ToList().ForEach(_database.Create);
        }
        public int FindCountBooksMoreThen(int countMoreThen, Func<Book, string> orederBy, int numberBooksInResult)
        {
            var queryBooks = _database.GetAll().Where(b => b.Count > countMoreThen)
                .OrderBy(orederBy)
                .Take(numberBooksInResult);
            //TODO: ask for question
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
            return _database.GetAll()
                            .Where(b => !String.IsNullOrEmpty(b.Author))
                            .Select(b => b.Author)
                            .Distinct()
                            .ToList();
        }
        public IEnumerable<Book> GetBooksWithoutAuthor()
        {
            return _database.GetAll()
                            .Where(b => String.IsNullOrEmpty(b.Author));
        }
        public void IncrementCountOfBookOn(int count)
        {
            var books = _database.GetAll();
            foreach (var book in books)
            {
                book.Count += count;
                _database.Update(book.Id, book);
            }
        }
        public void AddGengeToBookWith(Func<Book, bool> filter, string genreToAdd)
        {
            var filteredBooks = _database.GetAll().Where(filter);
            foreach (var book in filteredBooks)
            {
                if (!book.Genres.Contains(genreToAdd))
                {
                    book.Genres.Add(genreToAdd);
                    _database.Update(book.Id, book);
                }
            }
        }
        public void DeleteBooks(Func<Book, bool> filter)
        {
            _database.GetAll()
                     .Where(filter)
                     .ToList()
                     .ForEach(book => _database.Delete(book.Id));
        }
    }
}