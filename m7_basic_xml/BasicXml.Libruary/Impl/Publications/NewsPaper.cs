using System;
using System.Text;
using BasicXml.Library.Interfaces.Publications;

namespace BasicXml.Library.Impl.Publications
{
    public class NewsPaper:BaseBookNewsPaper
    {
        public string ISSN { get; set; }
        public DateTime DateOfPublication { get; set; }
        public int Number { get; set; }
        public override string ToString()
        {
            return new StringBuilder(base.ToString())
                .AppendLine($"City: {City}")
                .AppendLine($"Publisher: {Publisher}")
                .AppendLine($"Year: {Year}")
                .AppendLine($"Number: {Number}")
                .AppendLine($"Date of publication: {DateOfPublication:yyyy-MM-dd}")
                .AppendLine($"ISSN: {ISSN}")
                .ToString();
        }
    }
}