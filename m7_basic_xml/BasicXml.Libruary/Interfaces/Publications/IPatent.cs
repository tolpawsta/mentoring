using System;
using System.Collections.Generic;

namespace BasicXml.Libruary.Interfaces.Publications
{
    public interface IPatent
    {
        List<string> Inventors { get; set; }
        string RegistrationNummber { get; set;}
        DateTime DateOfApplication { get; set;}
        DateTime DateOfPublication { get; set; }
    }
}