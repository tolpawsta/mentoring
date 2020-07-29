using BasicXml.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace BasicXml.Library.Impl
{
    public class BasicXmlReader : IXmlReader
    {
        private XmlReader _reader;
        public DateTime DateOfPublish { get; private set; }
        public XElement GetRootElement(string pathToXmlFile)
        {
            using (var reader = XmlReader.Create(pathToXmlFile))
            {
                reader.MoveToContent();
                return (XElement) XNode.ReadFrom(reader);
            }
        }

        public IEnumerable<XElement> Read(string pathToXmlFile)
        {
            if (!File.Exists(pathToXmlFile))
            {
                throw new FileNotFoundException();
            }
            try
            {
                var settings = new XmlReaderSettings()
                {
                    IgnoreWhitespace = true
                };
                _reader = XmlReader.Create(pathToXmlFile, settings);
                _reader.MoveToContent();
                _reader.Read();
                do
                {
                    yield return XNode.ReadFrom(_reader) as XElement;
                } while (_reader.NodeType != XmlNodeType.EndElement);
            }
            finally
            {
                _reader?.Close();
            }
        }
    }
}