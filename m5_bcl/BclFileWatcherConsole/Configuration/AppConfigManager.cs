using BclFileWatcherConsole.Helpers;
using System;
using System.Configuration;
using System.IO;

namespace BclFileWatcherConsole.Configuration
{
    public static class AppConfigManager
    {
        public static AppConfigurationSection GetConfiguration(string name)
        {
            AppConfigurationSection config = null;
            try
            {
                config = (AppConfigurationSection) ConfigurationManager.GetSection(name);
                if (config == null)
                {
                    throw new ArgumentNullException();
                }
                config.Validate();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
            return config;
        }
    }
}
