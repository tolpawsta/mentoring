using System;
using System.Collections.Generic;

namespace BasicXml.Libruary.Interfaces.Publications
{
    public interface IPatent
    {
        List<string> Inventors { get; set; }
        string RegistrationNumber { get; set;}
        DateTime DateOfApplication { get; set;}
        DateTime DateOfPublication { get; set; }
        string Country { get; set; }
    }
}