using MongoBDCore.Interfaces;
using MongoDBConsole.Interfaces;
using System;

namespace MongoDBConsole
{
    public class MongoTask
    {
        private readonly IBookService _bookService;
        private readonly IView _view;
        private readonly IDataProvider dataProvider;

        public MongoTask(IBookService bookService, IView view, IDataProvider dataProvider)
        {
            _bookService = bookService;
            _view = view;
            this.dataProvider = dataProvider;
        }
        public void AddBooks()
        {
            Console.WriteLine($"Create books:");
            _bookService.AddRange(dataProvider.GetBooks());
            _view.Show(_bookService.GetAll());
        }
        public void GetCountBooksWithCountMoreThen()
        {
            var countMoreThen = 1;
            var numberBooksInResult = 3;
            var foundCountBooks = _bookService.FindCountBooksMoreThen(countMoreThen, b => b.Name, numberBooksInResult);
            Console.WriteLine($"Found count books wich count more than {countMoreThen} is {foundCountBooks}");
        }

        internal void DeleteBooks()
        {
            var lessThenCount = 6;
            Console.WriteLine("Book before deleteing");
            _view.Show(_bookService.GetAll());
            _bookService.DeleteBooks(b => b.Count < lessThenCount);
            Console.WriteLine("Book after deleteing");
            _view.Show(_bookService.GetAll());
            _bookService.DeleteBooks(_ => true);
            Console.WriteLine("After deleteing all books");
            _view.Show(_bookService.GetAll());
        }

        internal void AddGenreToBook()
        {
            var addingGenre = "favority";
            var filterGenre = "fantasy";
            Console.WriteLine($"Book before addition genre: '{addingGenre}'");
            _view.Show(_bookService.GetAll());
            Console.WriteLine($"First addition genre '{addingGenre}' to the books wich contains genre '{filterGenre}'");
            _bookService.AddGengeToBookWith(b => b.Genres.Contains(filterGenre), genreToAdd: addingGenre);
            Console.WriteLine($"Book after addition genre: '{addingGenre}'");
            _view.Show(_bookService.GetAll());
            Console.WriteLine($"Second addition genre '{addingGenre}' to the books wich contains genre '{filterGenre}'");
            _bookService.AddGengeToBookWith(b => b.Genres.Contains(filterGenre), genreToAdd: addingGenre);
            Console.WriteLine($"Book after addition genre: {addingGenre}");
            _view.Show(_bookService.GetAll());
        }

        internal void IncrementBooksCount()
        {
            var countToIncrement = 1;
            Console.WriteLine("Before increment book's count: ");
            _view.Show(_bookService.GetAll());
            _bookService.IncrementCountOfBookOn(countToIncrement);
            Console.WriteLine($"After increment book's count by {countToIncrement}");
            _view.Show(_bookService.GetAll());
        }

        internal void SelectBooksWithoutAuthor()
        {
            Console.WriteLine("Select books without author");
            var withoutAuthorBooks = _bookService.GetBooksWithoutAuthor();
            _view.Show(withoutAuthorBooks);
        }

        public void FindBookWithMaxCount()
        {
            Console.WriteLine("Find book with max count:");
            var bookMaxCount = _bookService.FindBookWithMaxCount();
            _view.Show(bookMaxCount);
        }
        public void FindAllAuthors()
        {
            Console.WriteLine("Find all authors");
            var authors = _bookService.GetAllAuthors();
            authors.ForEach(author => Console.WriteLine($"Author name is {author}"));
        }
    }
}
