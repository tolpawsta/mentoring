using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BasicXml.Library.Interfaces
{
    public interface IXmlReader
    {
        IEnumerable<XElement> Read(string pathToXmlFile);
        XElement GetRootElement(string pathToXmlFile);
    }
}