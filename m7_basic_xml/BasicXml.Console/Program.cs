using System;
using System.IO;
using System.Linq;
using Autofac;
using BasicXml.Console.Utils;
using BasicXml.Library.Impl;
using BasicXml.Library.Impl.Publications;
using static System.Console;

namespace BasicXml.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetFullPath($@"{AppDomain.CurrentDomain.BaseDirectory}\XmlFiles\Example1.xml");
            var filePathToWrite = "Example4.xml";
            var container = ContainerManager.CreateContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var library = scope.Resolve<XmlLibrary>();
                var viewer = scope.Resolve<BasicViewer>();
                var publications = library.Read(path).ToList();
                if (publications?.Count() > 0)
                {
                    WriteLine($"Found {publications.Count()} publications, which contains:");
                    WriteLine($"Books count is {publications.OfType<Book>().Count()}");
                    viewer.ShowPublicationElements(publications.OfType<Book>());
                    WriteLine($"Newspapers count is {publications.OfType<NewsPaper>().Count()}");
                    viewer.ShowPublicationElements(publications.OfType<NewsPaper>());
                    WriteLine($"Patents count is {publications.OfType<Patent>().Count()}");
                    viewer.ShowPublicationElements(publications.OfType<Patent>());
                }
                WriteLine($"Wrong publications {library.WrongPublications?.Count()}");
                viewer.ShowWrongElements(library.WrongPublications);
                    
                WriteLine($"Write publications to file: {filePathToWrite}");
                library.Write(publications, filePathToWrite);
            }
        }
    }
}
