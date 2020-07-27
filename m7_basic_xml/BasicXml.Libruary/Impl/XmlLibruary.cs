using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BasicXml.Libruary.Interfaces;
using BasicXml.Libruary.Interfaces.Publications;

namespace BasicXml.Libruary.Impl
{
    public class XmlLibruary : ILibruary
    {
        private IXmlReader _reader;
        private IXmlWriter _writer;

        public XmlLibruary(IXmlReader reader, IXmlWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }
        public IEnumerable<Publication> Read(StreamReader stream) => _reader.Read(stream);
        public void Write(IEnumerable<Publication> publications, string targetDocPath)
        {
            if (publications == null)
            {
                throw new ArgumentNullException();
            }
            if (publications.Count() == 0)
            {
                throw new ArgumentException("publications is empty");
            }
            _writer?.Write(publications, targetDocPath);
        }

        public void Write(IEnumerable<Publication> publications, Stream stream)
        {
            if (publications == null)
            {
                throw new ArgumentNullException();
            }
            if (publications.Count() == 0)
            {
                throw new ArgumentException("publications is empty");
            }

            _writer?.Write(publications, stream);
        }
    }
}