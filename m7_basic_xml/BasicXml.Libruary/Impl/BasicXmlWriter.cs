using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using BasicXml.Library.Impl.Publications;
using BasicXml.Library.Interfaces;
using BasicXml.Library.Interfaces.Publications;
using BasicXml.Library.Servises;

namespace BasicXml.Library.Impl
{
    public class BasicXmlWriter:IXmlWriter
    {
        private XmlWriter _writer;
        public string LibraryName { get; set; }
        public void Write(IEnumerable<Publication> publications, string targetDocPath)
        {
            if (!File.Exists(targetDocPath))
            {
                var fileStream = File.Create(targetDocPath);
                fileStream.Close();
            }
            var settings = new XmlWriterSettings()
            {
                Indent = true,
                NewLineOnAttributes = true
            };
            try
            {
                using (_writer = XmlWriter.Create(targetDocPath, settings))
                {
                    _writer.WriteStartDocument(true);
                    _writer.WriteStartElement(Constants.PUBLICATION);
                    _writer.WriteAttributeString(Constants.ELEMENT_NAME_PUBLISH_DATE,
                        DateTime.Now.ToShortDateString());
                    _writer.WriteAttributeString(Constants.ATTRIBUTE_NAME_LIBRARY,
                        LibraryName);
                    foreach (var publication in publications)
                    {
                        if (publication is Book book)
                        {
                            WriteBook(book);
                        }

                        if (publication is NewsPaper paper)
                        {
                            WriteNewsPaper(paper);
                        }

                        if (publication is Patent patent)
                        {
                            WritePatent(patent);
                        }
                    }
                    _writer.WriteEndElement();
                    _writer.Flush();
                }
            }
            catch (FileLoadException e)
            {
                Trace.TraceWarning(e.Message, e.StackTrace);
            }
            catch (IOException e)
            {
                Trace.TraceWarning(e.Message,e.StackTrace);
            }
        }

        private void WriteBook(Book book)
        {
            _writer.WriteStartElement(Constants.PUBLICATION_TYPE_BOOK);
            _writer.WriteElementString(Constants.ELEMENT_NAME_TITLE, book.Title);
            _writer.WriteStartElement(Constants.ELEMENT_NAME_AUTHORS);
            foreach (var author in book.Authors)
            {
                _writer.WriteElementString(Constants.ELEMENT_NAME_AUTHOR, author);
            }
            _writer.WriteEndElement();
            _writer.WriteElementString(Constants.ELEMENT_NAME_CITY, book.City);
            _writer.WriteElementString(Constants.ELEMENT_NAME_PUBLISHER, book.Publisher);
            _writer.WriteElementString(Constants.ELEMENT_NAME_YEAR, book.Year.ToString());
            _writer.WriteElementString(Constants.ELEMENT_NAME_PAGES, book.NumberOfPages.ToString());
            _writer.WriteElementString(Constants.ELEMENT_NAME_ISBN, book.ISBN);
            _writer.WriteElementString(Constants.ELEMENT_NAME_NOTE, book.Note);
            _writer.WriteEndElement();
        }

        private void WriteNewsPaper(NewsPaper newsPaper)
        {
            _writer.WriteStartElement(Constants.PUBLICATION_TYPE_NEWSPAPER);
            _writer.WriteElementString(Constants.ELEMENT_NAME_TITLE, newsPaper.Title);
            _writer.WriteElementString(Constants.ELEMENT_NAME_CITY, newsPaper.City);
            _writer.WriteElementString(Constants.ELEMENT_NAME_PUBLISHER, newsPaper.Publisher);
            _writer.WriteElementString(Constants.ELEMENT_NAME_YEAR, newsPaper.Year.ToString());
            _writer.WriteElementString(Constants.ELEMENT_NAME_PAGES, newsPaper.NumberOfPages.ToString());
            _writer.WriteElementString(Constants.ELEMENT_NAME_PUBLISH_DATE, newsPaper.DateOfPublication.ToShortDateString()); _writer.WriteElementString(Constants.ELEMENT_NAME_ISBN, newsPaper.ISSN);
            _writer.WriteElementString(Constants.ELEMENT_NAME_NOTE, newsPaper.Note);
            _writer.WriteEndElement();
        }

        private void WritePatent(Patent patent)
        {
            _writer.WriteStartElement(Constants.PUBLICATION_TYPE_PATENT);
            _writer.WriteElementString(Constants.ELEMENT_NAME_TITLE, patent.Title);
            _writer.WriteStartElement(Constants.ELEMENT_NAME_INVENTORS);
            foreach (var inventor in patent.Inventors)
            {
                _writer.WriteElementString(Constants.ELEMENT_NAME_INVENTOR, inventor);
            }
            _writer.WriteEndElement();
            _writer.WriteElementString(Constants.ELEMENT_NAME_COUNTRY, patent.Country);
            _writer.WriteElementString(Constants.ELEMENT_NAME_REGISTRATION_NUMBER, patent.RegistrationNumber);
            _writer.WriteElementString(Constants.ELEMENT_NAME_APPLICATION_DATE, patent.DateOfApplication.ToShortDateString());
            _writer.WriteElementString(Constants.ELEMENT_NAME_PUBLICATION_DATE, patent.DateOfPublication.ToShortDateString());
            _writer.WriteElementString(Constants.ELEMENT_NAME_PAGES, patent.NumberOfPages.ToString());
            _writer.WriteElementString(Constants.ELEMENT_NAME_NOTE, patent.Note);
            _writer.WriteEndElement();
        }
    }
}
