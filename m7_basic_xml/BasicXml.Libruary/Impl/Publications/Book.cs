using System.Collections.Generic;
using System.Text;
using BasicXml.Library.Interfaces.Publications;

namespace BasicXml.Library.Impl.Publications
{
    public class Book:BaseBookNewsPaper
    {
        public string ISBN { get; set; }
        public List<string> Authors { get; set; }
        public override string ToString()
        {
            return new StringBuilder(base.ToString())
                .AppendLine($"Authors: ")
                .AppendJoin("\t\n",Authors)
                .AppendLine($"\nCity: {City}")
                .AppendLine($"Publisher: {Publisher}")
                .AppendLine($"Year: {Year}")
                .AppendLine($"ISBN: {ISBN}")
                .ToString();
        }
    }
}