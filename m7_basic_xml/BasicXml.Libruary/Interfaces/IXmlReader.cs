using System;
using System.Collections.Generic;
using System.IO;
using BasicXml.Libruary.Interfaces.Publications;

namespace BasicXml.Libruary.Interfaces
{
    public interface IXmlReader
    {
        IEnumerable<Publication> Read(StreamReader stream);
        DateTime DateOfPublish { get;}
        string NameLibruary { get; }
        string PathToXsdFile { get; set; }
    }
}