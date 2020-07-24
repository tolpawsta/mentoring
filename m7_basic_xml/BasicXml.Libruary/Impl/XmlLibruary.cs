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
        public IXmlDocument Read(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream is null");
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Stream can not read");
            }
            return _reader.Read(stream);
        }

        public IXmlDocument Read(string pathFile)
        {
            if (!File.Exists(pathFile))
            {
                throw new FileNotFoundException();
            }

            return _reader.Read(pathFile);
        }

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