using BclFileWatcherConsole.Helpers;
using System;
using System.Configuration;
using System.IO;

namespace BclFileWatcherConsole.Configuration
{
    public static class AppConfigManager
    {
        public static AppConfigurationSection GetConfiguration(string name)
        {
            AppConfigurationSection config = null;
            config = (AppConfigurationSection) ConfigurationManager.GetSection(name);
            return config;
        }
    }
}
