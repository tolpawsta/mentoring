using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoBDCore.Interfaces;
using MongoBDCore.Models;
using MongoBDCore.Models.Configuration;
using MongoBDCore.Repositories;
using MongoBDCore.Services;
using MongoDBConsole.Constants;
using MongoDBConsole.DataProviders;
using System.Configuration;

namespace MongoDBConsole.AppConfiguration
{
    public static class ConfigarationRoot
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(ConfigConstants.JSONFILENAME)
                .Build();            
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = configuration.GetSection(ConfigConstants.CONNECTIONSTRING).Value;
                options.DatabaseName = configuration.GetSection(ConfigConstants.DATABASENAME).Value;
            });
            services.AddTransient<IAppContext, AppContext>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<BooksDataProvider>();
            services.AddTransient<ConsoleView>();
            services.AddTransient<MongoTask>();
        }
    }
}
