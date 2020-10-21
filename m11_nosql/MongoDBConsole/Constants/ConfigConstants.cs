using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBConsole.Constants
{
    public static class ConfigConstants
    {
        public static string CONNECTION_STRING = "MongoConnection:ConnectionString";
        public static string DATABASE_NAME = "MongoConnection:DatabaseName";
        public static string JSON_FILE_NAME = "AppSettings.json";
    }
}
