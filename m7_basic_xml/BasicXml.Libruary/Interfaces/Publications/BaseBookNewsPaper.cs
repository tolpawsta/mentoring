namespace BasicXml.Libruary.Interfaces.Publications
{
    public abstract class BaseBookNewsPaper : Publication
    {
        public string City { get; set; }
        public string PlaceOfPublication { get; set; }
        public string NameOfPublisher { get; set; }
        public uint Year { get; set; }
    }
}