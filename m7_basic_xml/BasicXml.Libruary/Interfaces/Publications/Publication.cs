using System.Text;

namespace BasicXml.Library.Interfaces.Publications
{
    public abstract class Publication
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public int NumberOfPages { get; set; }
        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Title: {Title}")
                .AppendLine($"Pages: {NumberOfPages}")
                .AppendLine($"Note: {Note}")
                .ToString();
        }
    }
}