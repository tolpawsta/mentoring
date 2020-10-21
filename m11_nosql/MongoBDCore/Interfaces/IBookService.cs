using MongoBDCore.Models;
using System;
using System.Collections.Generic;

namespace MongoBDCore.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAll();
        void AddGengeToBookWith(Func<Book, bool> filter, string genreToAdd);
        void AddRange(IEnumerable<Book> books);
        void Add(Book book);
        void DeleteBooks(Func<Book, bool> filter);
        List<string> GetAllAuthors();
        IEnumerable<Book> GetBooksWithoutAuthor();
        Book FindBookWithMaxCount();
        int FindCountBooksMoreThen(int countMoreThen, Func<Book, string> property, int numberBooksInResult);
        void IncrementCountOfBookOn(int count);
    }
}