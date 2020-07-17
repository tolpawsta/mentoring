using System.Configuration;

namespace BclFileWatcherConsole.Configuration
{
    public class DefaultDirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true)]
        [StringValidator(InvalidCharacters = "")]
        public string Path => (string) this["path"];
    }
}
