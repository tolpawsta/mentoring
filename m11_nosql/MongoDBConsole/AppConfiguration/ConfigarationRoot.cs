using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoBDCore.Interfaces;
using MongoBDCore.Models;
using MongoBDCore.Models.Configuration;
using MongoBDCore.Repositories;
using MongoBDCore.Services;
using MongoDBConsole.Constants;
using MongoDBConsole.DataProviders;
using MongoDBConsole.Interfaces;
using System.Configuration;

namespace MongoDBConsole.AppConfiguration
{
    public static class ConfigarationRoot
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(ConfigConstants.JSON_FILE_NAME)
                .Build();            
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = configuration.GetSection(ConfigConstants.CONNECTION_STRING).Value;
                options.DatabaseName = configuration.GetSection(ConfigConstants.DATABASE_NAME).Value;
            });
            services.AddTransient<IAppContext, AppContext>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IDataProvider,BooksDataProvider>();
            services.AddTransient<IView,ConsoleView>();
            services.AddTransient<MongoTask>();
        }
    }
}
