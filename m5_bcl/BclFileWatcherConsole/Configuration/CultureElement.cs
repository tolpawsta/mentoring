using System.Configuration;

namespace BclFileWatcherConsole.Configuration
{
    public class CultureElement:ConfigurationElement
    {
        [ConfigurationProperty("name",DefaultValue ="en-US")]
        [StringValidator(InvalidCharacters =null)]
        public string Name => (string) this["name"];
    }
}
