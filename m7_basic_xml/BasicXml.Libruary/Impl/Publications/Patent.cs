using BasicXml.Libruary.Interfaces.Publications;
using System;
using System.Collections.Generic;

namespace BasicXml.Libruary.Impl.Publications
{
    public class Patent:Publication, IPatent
    {
        public List<string> Inventors { get; set; }
        public string RegistrationNummber { get; set; }
        public DateTime DateOfApplication { get; set; }
        public DateTime DateOfPublication { get; set; }
    }
}