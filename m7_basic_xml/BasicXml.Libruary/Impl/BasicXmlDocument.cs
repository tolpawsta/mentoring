using System;
using System.Collections.Generic;
using BasicXml.Libruary.Impl.Publications;
using BasicXml.Libruary.Interfaces;
using BasicXml.Libruary.Interfaces.Publications;

namespace BasicXml.Libruary.Impl
{
    public class BasicXmlDocument:IXmlDocument
    {
        public string NameLibruary { get; set; }
        public DateTime DateOfPublish { get; set; }
        public IEnumerable<Publication> Puplications { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<NewsPaper> NewsPapers { get; set; }
        public IEnumerable<Patent> Patents { get; set; }
    }
}