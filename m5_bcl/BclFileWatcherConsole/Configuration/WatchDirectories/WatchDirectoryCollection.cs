using System.Configuration;

namespace BclFileWatcherConsole.Configuration.WatchDirectories
{
    public class WatchDirectoryCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
           return new WatchDirectoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WatchDirectoryElement) element).Path;
        }
    }
}
