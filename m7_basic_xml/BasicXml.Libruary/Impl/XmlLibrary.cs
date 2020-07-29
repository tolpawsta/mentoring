using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BasicXml.Library.Interfaces;
using BasicXml.Library.Interfaces.Publications;

namespace BasicXml.Library.Impl
{
    public class XmlLibrary 
    {
        private IXmlReader _reader;
        private IXmlWriter _writer;
        private IXmlParser _parser;
        public List<Publication> WrongPublications { get; private set; }
        public string DocumentLibruaryName { get; private set; }
        public string DocumentPublishDate { get; private set; }

        public XmlLibrary(IXmlReader reader, IXmlWriter writer, IXmlParser parser)
        {
            _reader = reader;
            _writer = writer;
            _parser = parser;
            WrongPublications=new List<Publication>();
        }

        public IEnumerable<Publication> Read(string pathToXmlFile)
        {
            var rootElement = _reader.GetRootElement(pathToXmlFile);
            DocumentPublishDate = _parser.GetPublishDate(rootElement);
            DocumentLibruaryName = _parser.GetLibraryName(rootElement);
            foreach (var element in _reader.Read(pathToXmlFile))
            {
                var publication = _parser.ParseToPublication(element);
                if (!_parser.WasParseSuccessful)
                {
                    WrongPublications.Add(publication);
                    continue;
                }
                yield return publication;
            }
        }

        public void Write(IEnumerable<Publication> publications, string targetDocPath)
        {
            if (publications == null)
            {
                throw new ArgumentNullException();
            }
            _writer.LibraryName = this.GetType().Name;
            _writer?.Write(publications, targetDocPath);
        }
    }
}