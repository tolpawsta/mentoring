using BclFileWatcherConsole.Configuration;
using BclFileWatcherConsole.Configuration.WatchDirectories;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using messages = BclFileWatcherConsole.Resources.Messages;
namespace BclFileWatcherConsole.Helpers
{
    public static class AppConfigValidator
    {
         
        public static void Validate(this AppConfigurationSection config)
        {
            try
            {
                if (!Directory.Exists(config.DefaultDirectory.Path))
                {
                    throw new DirectoryNotFoundException(String.Concat(messages.Attempted_access_to_the_puth, $": {config.DefaultDirectory.Path}"));
                }
                if (config.WatchDirectories.Count > 0)
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
                if (config.Rules.Count == 0)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
