using System.Collections.Generic;
using System.Xml;
using BasicXml.Libruary.Interfaces.Publications;

namespace BasicXml.Libruary.Interfaces
{
    public interface IXmlParser
    {
        Publication GetPublication(XmlReader reader);
    }
}