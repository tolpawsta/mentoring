using System;
using System.IO;
using System.Linq;
using BasicXml.Libruary.Impl;
using static System.Console;

namespace BasicXml.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var path=Path.GetFullPath($@"{AppDomain.CurrentDomain.BaseDirectory}\XmlFiles\Example1.xml");
            var parser=new BasicXmlParser();
            var reader = new BasicXmlReader(parser);
            reader.PathToXsdFile= Path.GetFullPath($@"{AppDomain.CurrentDomain.BaseDirectory}\XmlSchema\publications.xsd");
            var writer=new BasicXmlWriter();
            var libruary=new XmlLibruary(reader,writer);
            using (var stream = new StreamReader(path))
            {
                var publications = libruary.Read(stream).ToList();
                publications.ForEach(p => WriteLine($"Title: {p.Title} Pages {p.NumberOfPages} Note {p.Note}"));
            }
        }
    }
}
