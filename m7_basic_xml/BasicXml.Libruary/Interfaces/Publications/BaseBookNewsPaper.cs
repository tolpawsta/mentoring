namespace BasicXml.Library.Interfaces.Publications
{
    public abstract class BaseBookNewsPaper : Publication
    {
        public string City { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
    }
}