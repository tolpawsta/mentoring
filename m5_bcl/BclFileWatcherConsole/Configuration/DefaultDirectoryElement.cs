using System.Configuration;

namespace BclFileWatcherConsole.Configuration
{
    public class DefaultDirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true)]
        [StringValidator(InvalidCharacters = "", MaxLength = 4)]
        public string Path => (string) this["path"];
    }
}
