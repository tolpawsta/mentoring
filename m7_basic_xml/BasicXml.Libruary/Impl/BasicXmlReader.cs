using BasicXml.Libruary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
        private IXmlDocument _document;
        public string PathToXsdFile { get; set; }

        public BasicXmlReader()
        {

        }

        public IXmlDocument Read(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                return Read(streamReader);
            }
        }

        public IXmlDocument Read(string pathFile)
        {
            var stream = new FileStream(pathFile, FileMode.Open);
            return Read(stream);
        }

        private IXmlDocument Read(StreamReader stream)
        {
            _document = new BasicXmlDocument();
            XmlReaderSettings settings = null;
            try
            {
                settings = XmlReaderService.GetSettings(PathToXsdFile);
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
                    _document.DateOfPublish = DateTime.Parse(_reader.GetAttribute("publishDate"));
                    _document.NameLibruary = _reader.GetAttribute("nameLibruary");
                }
                var publications = new List<Publication>();
                _reader.MoveToElement();
                while (_reader.ReadToNextSibling("book") || _reader.ReadToNextSibling("newspaper") ||
                       _reader.ReadToNextSibling("patent"))
                {
                    if (_reader.Name == "book")
                    {
                        var book = new Book();
                        _reader.ReadToDescendant("title");
                        book.Title = _reader.ReadElementContentAsString();
                        _reader.ReadToFollowing("authors");
                        _reader.ReadToDescendant("author");
                        do
                        {

                            book.Authors = new List<string>();
                            var author = _reader.ReadElementContentAsString();
                            if (author != null)
                            {
                                book.Authors.Add(author);
                            }
                        } while (_reader.ReadToNextSibling("author"));

                        _reader.ReadToFollowing("city");
                        book.City = _reader.ReadElementContentAsString();
                        publications.Add(book);
                    }
                    else if (_reader.Name == "newspaper")
                    {

                    }
                    else if (_reader.Name == "patent")
                    {

                    }
                    else
                    {

                    }

                    Console.WriteLine($"Element {_reader.Name} value {_reader.ReadContentAsString()}");
                }
                _document.Puplications = publications;
                Console.WriteLine("Validate Successfull");

            }
            finally
            {
                _reader?.Close();
            }

            return _document;
        }

        private void ValidationEventHandle(object sender, ValidationEventArgs e)
        {
            Console.WriteLine($"Validation error: {e.Message}: {e.Exception.LinePosition} ");
        }
    }
}