using BclFileWatcherConsole.Configuration.Rules;
using BclFileWatcherConsole.Configuration.WatchDirectories;
using System.Configuration;

namespace BclFileWatcherConsole.Configuration
{
    public class UserAppCofiguration : ConfigurationSection
    {
        [ConfigurationProperty("appName")]
        public string ApplicationName => (string) base["appName"];
        [ConfigurationProperty("culture")]
        public CultureElement Culture => (CultureElement) this["culture"];
        [ConfigurationProperty("defaultDirectory")]
        public DefaultDirectoryElement DefaultDirectory => (DefaultDirectoryElement) this["defaultDirectory"];
        [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
        [ConfigurationProperty("rules")]
        public RuleElementCollection Rule => (RuleElementCollection) this["rules"];
        [ConfigurationCollection(typeof(WatchDirectoryElement), AddItemName = "dir")]
        [ConfigurationProperty("directories")]
        public WatchDirectoryCollection WatchDirectories => (WatchDirectoryCollection) this["directories"];
    }
}
