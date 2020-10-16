using MongoBDCore.Models;
using System.Collections.Generic;

namespace MongoBDCore.Interfaces
{
    public interface IBookRepository
    {
        public List<Book> GetAll();
        public Book GetBook(string id);
        public void Create(Book book);
        public void Update(string id, Book book);
        public void Delete(string id);
    }
}
