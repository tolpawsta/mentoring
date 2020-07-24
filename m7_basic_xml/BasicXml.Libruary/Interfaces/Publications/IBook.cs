using System.Collections.Generic;

namespace BasicXml.Libruary.Interfaces.Publications
{
    public interface IBook
    {
       string ISBN { get; set; }
       List<string> Authors { get; set; }
    }
}