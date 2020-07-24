using System.Collections.Generic;
using BasicXml.Libruary.Interfaces.Publications;

namespace BasicXml.Libruary.Interfaces
{
    public interface IXmlParser
    {
        Publication Publications { get; }
    }
}