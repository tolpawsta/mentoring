using System;
using System.Collections.Generic;
using System.Diagnostics;
using BasicXml.Library.Impl.Publications;
using BasicXml.Library.Interfaces;
using BasicXml.Library.Interfaces.Publications;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BasicXml.Library.Servises;

namespace BasicXml.Library.Impl
{
    public class BasicXmlParser : IXmlParser
    {
       public bool WasParseSuccessful { get; private set; }

        public Publication ParseToPublication(XElement element)
        {
            WasParseSuccessful = true;
            Publication publication = null;
            try
            {
                switch (element.Name.LocalName)
                {
                    case ConstantsService.PUBLICATION_TYPE_BOOK:
                        publication = ParseToBook(element);
                        break;
                    case ConstantsService.PUBLICATION_TYPE_NEWSPAPER:
                        publication = ParseToNewsPaper(element);
                        break;
                    case ConstantsService.PUBLICATION_TYPE_PATENT:
                        publication = ParseToPatent(element);
                        break;
                    default:
                        WasParseSuccessful = false;
                        break;
                }
            }
            catch (FormatException e)
            {
                Trace.TraceWarning(e.Message, e.StackTrace);
                WasParseSuccessful = false;
            }
            catch (XmlException e)
            {
                Trace.TraceWarning(e.Message, e.StackTrace);
                WasParseSuccessful = false;
            }
            catch (NullReferenceException e)
            {
                Trace.TraceWarning(e.Message, e.StackTrace);
                WasParseSuccessful = false;
            }
            return publication;
        }

        private Book ParseToBook(XElement element)
        {
            var book = new Book();
            book.Title = element.GetStringValue(ConstantsService.ELEMENT_NAME_TITLE);
            book.Authors = element.GetListSubElements(ConstantsService.ELEMENT_NAME_AUTHOR);
            book.City = element.GetStringValue(ConstantsService.ELEMENT_NAME_CITY);
            book.Publisher = element.GetStringValue(ConstantsService.ELEMENT_NAME_PUBLISHER);
            book.Year = int.Parse(element.GetStringValue(ConstantsService.ELEMENT_NAME_YEAR));
            book.NumberOfPages = int.Parse(element.GetStringValue(ConstantsService.ELEMENT_NAME_PAGES));
            book.ISBN = element.GetStringValue(ConstantsService.ELEMENT_NAME_ISBN);
            book.Note = element.GetStringValue(ConstantsService.ELEMENT_NAME_NOTE);
            if (book.Authors?.Count() == 0 || book.Year <= 0
                                          || book.NumberOfPages <= 0
                                          || string.IsNullOrEmpty(book.Title))
            {
                WasParseSuccessful = false;
            }
            return book;
        }

        private NewsPaper ParseToNewsPaper(XElement element)
        {
            var newsPaper = new NewsPaper();
            newsPaper.Title = element.GetStringValue(ConstantsService.ELEMENT_NAME_TITLE);
            newsPaper.City = element.GetStringValue(ConstantsService.ELEMENT_NAME_CITY);
            newsPaper.Publisher = element.GetStringValue(ConstantsService.ELEMENT_NAME_PUBLISHER);
            newsPaper.Year = int.Parse(element.GetStringValue(ConstantsService.ELEMENT_NAME_YEAR));
            newsPaper.NumberOfPages = int.Parse(element.GetStringValue(ConstantsService.ELEMENT_NAME_PAGES));
            newsPaper.Number = int.Parse(element.GetStringValue(ConstantsService.ELEMENT_NAME_NUMBER));
            newsPaper.DateOfPublication = element.GetDate(ConstantsService.ELEMENT_NAME_PUBLISH_DATE);
            newsPaper.ISSN = element.GetStringValue(ConstantsService.ELEMENT_NAME_ISSN);
            newsPaper.Note = element.GetStringValue(ConstantsService.ELEMENT_NAME_NOTE);
            if (newsPaper.Year <= 0
                || newsPaper.NumberOfPages <= 0
                || newsPaper.Number <= 0
                || string.IsNullOrEmpty(newsPaper.Title)
                || newsPaper.DateOfPublication == null)
            {
                WasParseSuccessful = false;
            }
            return newsPaper;
        }

        private Patent ParseToPatent(XElement element)
        {
            var patent = new Patent();
            patent.Title = element.GetStringValue(ConstantsService.ELEMENT_NAME_TITLE);
            patent.Inventors = element.GetListSubElements(ConstantsService.ELEMENT_NAME_INVENTOR);
            patent.Country = element.GetStringValue(ConstantsService.ELEMENT_NAME_COUNTRY);
            patent.RegistrationNumber = element.GetStringValue(ConstantsService.ELEMENT_NAME_REGISTRATION_NUMBER);
            patent.DateOfApplication = element.GetDate(ConstantsService.ELEMENT_NAME_APPLICATION_DATE);
            patent.DateOfPublication = element.GetDate(ConstantsService.ELEMENT_NAME_PUBLICATION_DATE);
            patent.NumberOfPages = element.GetInt(ConstantsService.ELEMENT_NAME_PAGES);
            patent.Note = element.GetStringValue(ConstantsService.ELEMENT_NAME_NOTE);
            if (string.IsNullOrEmpty(patent.Title)
                || patent.Inventors?.Count() <= 0
                || patent.NumberOfPages <= 0
                || string.IsNullOrEmpty(patent.Title)
                || patent.DateOfPublication == null
                || patent.DateOfApplication == null)
            {
                WasParseSuccessful = false;
            }
            return patent;
        }
        public string GetPublishDate(XElement element)
        {
            return element.Attribute(ConstantsService.ELEMENT_NAME_PUBLISH_DATE)?.Value;
        }

        public string GetLibraryName(XElement element)
        {
            return element.Attribute(ConstantsService.ATTRIBUTE_NAME_LIBRARY)?.Value;
        }
    }
}