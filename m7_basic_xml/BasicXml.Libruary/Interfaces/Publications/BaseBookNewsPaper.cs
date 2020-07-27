namespace BasicXml.Libruary.Interfaces.Publications
{
    public abstract class BaseBookNewsPaper : Publication
    {
        public string City { get; set; }
        public string Publisher { get; set; }
        public uint Year { get; set; }
    }
}