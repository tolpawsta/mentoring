using BasicXml.Library.Interfaces.Publications;
using System.Xml.Linq;

namespace BasicXml.Library.Interfaces
{
    public interface IXmlParser
    {
        Publication ParseToPublication(XElement node);
        string GetPublishDate(XElement rootElement);
        string GetLibraryName(XElement rootElement);
        bool WasParseSuccessful { get; }
    }
}