using BasicXml.Libruary.Interfaces.Publications;
using System.Collections.Generic;

namespace BasicXml.Libruary.Impl.Publications
{
    public class Book:BaseBookNewsPaper,IBook
    {
        public string ISBN { get; set; }
        public List<string> Authors { get; set; }
    }
}