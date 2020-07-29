using System.Collections.Generic;
using System.IO;
using BasicXml.Library.Interfaces.Publications;

namespace BasicXml.Library.Interfaces
{
    public interface IXmlWriter
    {
        void Write(IEnumerable<Publication> publications, string targetDocPath);
        public string LibraryName { get; set; }
    }
}