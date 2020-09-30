namespace NorthwindDAL.Interfaces
{
    public interface IDbAppConfiguration
    {
        public string  ConnectionString { get;}
        public string Provider { get; }
    }
}