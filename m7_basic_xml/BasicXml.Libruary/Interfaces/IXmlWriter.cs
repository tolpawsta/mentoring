using System.Collections.Generic;
using System.IO;
using BasicXml.Libruary.Interfaces.Publications;

namespace BasicXml.Libruary.Interfaces
{
    public interface IXmlWriter
    {
        void Write(IEnumerable<Publication> publications, string targetDocPath);
        void Write(IEnumerable<Publication> publications, Stream targetDocPath);
    }
}