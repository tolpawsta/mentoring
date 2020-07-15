using System.Configuration;

namespace BclFileWatcherConsole.Configuration.WatchDirectories
{
    public class WatchDirectoryElement:ConfigurationElement
    {
        [ConfigurationProperty("path",IsKey =true)]
        public string Path => (string) base["path"];
    }
}
