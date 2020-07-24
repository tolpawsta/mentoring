using System;

namespace BasicXml.Libruary.Interfaces.Publications
{
    public interface INewspaper
    {
        string ISSN { get; set; }
        DateTime DateOfPublication { get; set; }
    }
}