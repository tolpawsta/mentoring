using Microsoft.Extensions.DependencyInjection;
using MongoBDCore.Interfaces;
using MongoBDCore.Repositories;
using MongoDBConsole.AppConfiguration;
using MongoDBConsole.DataProviders;
using System;
using System.Linq;

namespace MongoDBConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigarationRoot.ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            var tasks = provider.GetService<MongoTask>();

            tasks.AddBooks();
           // tasks.GetCountBooksWithCountMoreThen();
           // tasks.FindBookWithMaxCount();
           // tasks.FindAllAuthors();
           // tasks.SelectBooksWithoutAuthor();
           // tasks.IncrementBooksCount();
           // tasks.AddGenreToBook();
           // tasks.DeleteBooks();    
        }        
    }
}
