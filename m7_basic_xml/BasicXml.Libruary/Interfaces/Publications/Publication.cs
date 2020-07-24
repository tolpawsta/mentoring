namespace BasicXml.Libruary.Interfaces.Publications
{
    public abstract class Publication
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public uint NumberOfPages { get; set; }
    }
}