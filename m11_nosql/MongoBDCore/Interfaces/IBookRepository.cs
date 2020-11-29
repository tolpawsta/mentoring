using MongoBDCore.Models;
using System;
using System.Collections.Generic;

namespace MongoBDCore.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book GetBook(string id);
        List<Book> GetFilteredBooks(Func<Book, bool> filter);
        List<object> GetProjection(Func<Book, bool> filter, Func<Book, object> projection);
        List<Book> GetBooksWithCountLessThen(int count);
        void Append(Book book);
        void Update(string id, Book book);
        void IncrementCountOfBooks(int count);
        void InsertGenge(Func<Book, bool> filter, string addedGenre);
        void Delete(string id);
        void DeleteMany(Func<Book, bool> filter);
    }
}
