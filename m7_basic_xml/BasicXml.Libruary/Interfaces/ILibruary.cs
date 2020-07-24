using System.Collections;
using System.Collections.Generic;
using System.IO;
using BasicXml.Libruary.Interfaces.Publications;

namespace BasicXml.Libruary.Interfaces
{
    public interface ILibruary
    {
        IXmlDocument Read(Stream stream);
        IXmlDocument Read(string pathFile);
        void Write(IEnumerable<Publication> publications, string targetDocPath);
        void Write(IEnumerable<Publication> publications, Stream stream);
    }
}