using BasicXml.Libruary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using BasicXml.Libruary.Impl.Publications;
using BasicXml.Libruary.Interfaces.Publications;
using BasicXml.Libruary.Servises;

namespace BasicXml.Libruary.Impl
{
    public class BasicXmlReader : IXmlReader
    {
        private XmlReader _reader;
        public DateTime DateOfPublish { get; private set; }
        public string NameLibruary { get; private set; }
        public string PathToXsdFile { get; set; }
        private IXmlParser _parser;

        public BasicXmlReader(IXmlParser parser)
        {
            _parser = parser;
        }
        public IEnumerable<Publication> Read(StreamReader stream)
        {
            XmlReaderSettings settings = null;
            try
            {
                settings = XmlReaderService.GetSettings(PathToXsdFile);
                settings.ValidationEventHandler += ValidationEventHandle;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
            }
            catch (FileLoadException e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
            }
            catch (XmlSchemaException e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
            }

            try
            {
                if (settings != null)
                {
                    _reader = XmlReader.Create(stream, settings);
                }
                else
                {
                    _reader = XmlReader.Create(stream);
                }
                _reader.MoveToContent();
                if (!_reader.HasAttributes)
                {
                    Console.WriteLine($"start element hasn't attributes");
                }
                else
                {
                    DateOfPublish = DateTime.Parse(_reader.GetAttribute("publishDate"));
                    NameLibruary = _reader.GetAttribute("nameLibruary");
                    _reader.GetAttribute("xmlns:p");
                }
                _reader.MoveToElement();
                if (settings==null)
                {
                    _reader.Read();
                }
                while(_reader.Read()&&_reader.NodeType!=XmlNodeType.EndElement)
                {
                    yield return _parser.GetPublication(_reader);
                    _reader.MoveToContent();
                    _reader.Read();
                } 
                Console.WriteLine("Validate Successfull");

            }
            finally
            {
                stream?.Close();
                _reader?.Close();
            }
        }

        private void ValidationEventHandle(object sender, ValidationEventArgs e)
        {
            Console.WriteLine($"Validation error: {e.Message}: {e.Exception.LinePosition} ");
        }
    }
}