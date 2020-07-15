using BclFileWatcherConsole.Configuration;
using BclFileWatcherConsole.Configuration.WatchDirectories;
using System;
using System.IO;
using messages = BclFileWatcherConsole.Resources.Messages;
namespace BclFileWatcherConsole.Helpers
{
    public static class AppConfigValidator
    {
        public static void Validate(this AppConfigurationSection config)
        {            
            if (!Directory.Exists(config.DefaultDirectory.Path))
            {
                throw new DirectoryNotFoundException(String.Concat(messages.Attempted_access_to_the_puth,$": {config.DefaultDirectory.Path}"));
            }
            if (String.IsNullOrEmpty(config.Culture.Name))
            {
                throw new ArgumentNullException();
            }
            if (config.WatchDirectories.Count>0)
            {
                foreach (WatchDirectoryElement dir in config.WatchDirectories)
                {
                    if (!Directory.Exists(Path.GetFullPath(dir.Path)))
                    {
                        Directory.CreateDirectory(dir.Path);
                    }
                }
            }
            else
            {
                throw new DirectoryNotFoundException();
            }
            if (config.Rules.Count==0)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
