using System;
using System.Collections.Generic;
using System.Text;
using BasicXml.Library.Interfaces.Publications;

namespace BasicXml.Library.Impl.Publications
{
    public class Patent:Publication
    {
        public List<string> Inventors { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime DateOfApplication { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string Country { get; set; }
        public override string ToString()
        {
            return new StringBuilder(base.ToString())
                .AppendLine($"Inventors: ")
                .AppendJoin("\t\n", Inventors)
                .AppendLine($"\nCountry: {Country}")
                .AppendLine($"Registration number: {RegistrationNumber}")
                .AppendLine($"Date of application: {DateOfApplication:yyyy-MM-dd}")
                .AppendLine($"Date of publication: {DateOfPublication:yyyy-MM-dd}")
                .ToString();
        }
    }
}