using Microsoft.Extensions.DependencyInjection;
using MongoDBConsole.AppConfiguration;

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
