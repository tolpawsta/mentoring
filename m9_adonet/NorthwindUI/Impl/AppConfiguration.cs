using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using NorthwindDAL.Interfaces;
using Microsoft.Extensions.Configuration;
namespace NorthwindPL.Impl
{
    public class AppConfiguration : IDbAppConfiguration
    {
        private ConnectionStringSettings _configSettings;

        public virtual string ConnectionString => _configSettings?.ConnectionString;


        public virtual string Provider => _configSettings?.ProviderName;

        public AppConfiguration(ConnectionStringSettings configSettings)
        {
            _configSettings = configSettings;
        }
    }
}