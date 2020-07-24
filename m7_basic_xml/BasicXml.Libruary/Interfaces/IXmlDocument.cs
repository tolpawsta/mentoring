using System;
using System.Collections.Generic;
using BasicXml.Libruary.Impl.Publications;
using BasicXml.Libruary.Interfaces.Publications;

namespace BasicXml.Libruary.Interfaces
{
    public interface IXmlDocument
    {
        string NameLibruary { get; set; }
        DateTime DateOfPublish { get; set; }
        IEnumerable<Publication> Puplications { get; set; }
        IEnumerable<Book> Books { get; set; }
        IEnumerable<NewsPaper> NewsPapers { get; set; }
        IEnumerable<Patent> Patents { get; set; }

    }
}