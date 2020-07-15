using BclFileWatcherConsole.Configuration.Rules;
using BclFileWatcherConsole.Configuration.WatchDirectories;
using System.Configuration;

namespace BclFileWatcherConsole.Configuration
{
    public class AppConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("appName")]
        public string ApplicationName { get { return (string) base["appName"]; } }
        [ConfigurationProperty("culture")]
        public CultureElement Culture => (CultureElement) base["culture"];
        [ConfigurationProperty("defaultDirectory", IsRequired = true)]
        public DefaultDirectoryElement DefaultDirectory => (DefaultDirectoryElement) base["defaultDirectory"];
        [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
        [ConfigurationProperty("rules")]
        public RuleElementCollection Rules => (RuleElementCollection) base["rules"];
        [ConfigurationCollection(typeof(WatchDirectoryElement), AddItemName = "dir")]
        [ConfigurationProperty("directories")]
        public WatchDirectoryCollection WatchDirectories => (WatchDirectoryCollection) base["directories"];
    }
}
