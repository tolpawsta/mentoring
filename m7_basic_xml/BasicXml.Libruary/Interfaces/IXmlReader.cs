using System.Collections.Generic;
using System.IO;
using BasicXml.Libruary.Interfaces.Publications;

namespace BasicXml.Libruary.Interfaces
{
    public interface IXmlReader
    {
        IXmlDocument Read(Stream stream);
        IXmlDocument Read(string pathFile);
        string PathToXsdFile { get; set; }
    }
}