using BasicXml.Libruary.Interfaces.Publications;
using System;

namespace BasicXml.Libruary.Impl.Publications
{
    public class NewsPaper:BaseBookNewsPaper,INewspaper
    {
        public string ISSN { get; set; }
        public DateTime DateOfPublication { get; set; }
    }
}