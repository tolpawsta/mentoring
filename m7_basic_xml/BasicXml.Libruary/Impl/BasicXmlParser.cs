using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Linq;
using BasicXml.Libruary.Impl.Publications;
using BasicXml.Libruary.Interfaces;
using BasicXml.Libruary.Interfaces.Publications;
using BasicXml.Libruary.Servises;

namespace BasicXml.Libruary.Impl
{
    public class BasicXmlParser : IXmlParser
    {
        public Publication GetPublication(XmlReader reader)
        {
            switch (reader.Name)
            {
                case "book":
                    {
                        var book = new Book();
                        book.Title = reader.GetStringValue("title");
                        reader.ReadToFollowing("authors");
                        var element = XNode.ReadFrom(reader) as XElement;
                        if (element.HasElements)
                        {
                            book.Authors = new List<string>();
                            element.Descendants("author").ToList().ForEach(e => book.Authors.Add(e.Value));
                        }
                        book.City = reader.GetStringValue("city");
                        book.Publisher = reader.GetStringValue("publisher");
                        book.Year = reader.GetUint("year");
                        book.NumberOfPages = reader.GetUint("pages");
                        book.ISBN = reader.GetStringValue("isbn");
                        book.Note = reader.GetStringValue("note");
                        return book;
                    };
                case "newspaper":
                    {
                        var newsPaper = new NewsPaper();
                        //reader.ReadToDescendant("title");
                        newsPaper.Title = reader.GetStringValue("title");
                        newsPaper.City = reader.GetStringValue("city"); 
                        newsPaper.Publisher = reader.GetStringValue("publisher");
                        newsPaper.Year = reader.GetUint("year");
                        newsPaper.NumberOfPages = reader.GetUint("pages");
                        newsPaper.Number = reader.GetUint("number");
                        newsPaper.DateOfPublication = reader.GetDate("publishDate");
                        newsPaper.ISSN = reader.GetStringValue("issn");
                        newsPaper.Note = reader.GetStringValue("note");
                        return newsPaper;
                    };
                case "patent":
                    {
                        var patent = new Patent();
                        patent.Title = reader.GetStringValue("title");
                        reader.ReadToFollowing("inventors");
                        var element = XNode.ReadFrom(reader) as XElement;
                        if (element.HasElements)
                        {
                            patent.Inventors = new List<string>();
                            element.Descendants("inventor").ToList().ForEach(e => patent.Inventors.Add(e.Value));
                        }
                        patent.Country = reader.GetStringValue("country");
                        patent.RegistrationNumber = reader.GetStringValue("registrNumber");
                        patent.DateOfApplication = reader.GetDate("applicationDate");
                        patent.DateOfPublication = reader.GetDate("publicationDate");
                        patent.NumberOfPages = reader.GetUint("pages");
                        patent.Note = reader.GetStringValue("note");
                        return patent;
                    };
                default:
                    return null;
            }
        }
    }
}