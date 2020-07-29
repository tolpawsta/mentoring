using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Schema;

namespace BasicXml.Library.Servises
{
    public static class XmlReaderService
    {
        public static XmlReaderSettings GetSettings(string pathToXsd)
        {
            if (!File.Exists(pathToXsd))
            {
                throw new FileNotFoundException();
            }
            var settings = new XmlReaderSettings()
                {
                    ValidationType = ValidationType.Schema,
                    ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings,
                    IgnoreWhitespace = true,
                };
                settings.Schemas.Add(null, pathToXsd);
            return settings;
        }
    }
}