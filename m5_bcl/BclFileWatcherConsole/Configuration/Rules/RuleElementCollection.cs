using System.Configuration;

namespace BclFileWatcherConsole.Configuration.Rules
{
    public class RuleElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement) element).FileNamePattern;
        }
    }
}
