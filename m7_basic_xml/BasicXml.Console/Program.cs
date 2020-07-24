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
            var reader = new BasicXmlReader();
            var writer=new BasicXmlWriter();
            var libruary=new XmlLibruary(reader,writer);
            var document=libruary.Read(path);

            WriteLine($"Name libruary is {document.NameLibruary}, date of publishing is {document.DateOfPublish}");
            if (document.Puplications.Count()>0)
            {
                document.Puplications.ToList()
                    .ForEach(p=>WriteLine($"Title: {p.Title} Pages {p.NumberOfPages} Note {p.Note}"));
            }
        }
    }
}
