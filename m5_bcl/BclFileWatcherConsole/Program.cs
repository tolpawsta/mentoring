using BclFileWatcherConsole.Configuration;
using BclFileWatcherConsole.Configuration.WatchDirectories;
using BclFileWatcherConsole.Helpers;
using BclFileWatcherConsole.Impl;
using BclFileWatcherConsole.Impl.Listeners;
using BclFileWatcherConsole.Impl.Watchers;
using BclFileWatcherConsole.Interfaces;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using messages = BclFileWatcherConsole.Resources.Messages;
namespace BclFileWatcherConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            var nameConfigSection = ConfigurationManager.AppSettings.Get("SectionName");
            try
            {
                Run(nameConfigSection);                                               
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
        private static void Run(string nameConfigSection)
        {
            if (String.IsNullOrEmpty(nameConfigSection))
            {
                throw new NullReferenceException($"{messages.Section_in_config__do_not_exists}: {nameConfigSection}");
            }
            var config = AppConfigManager.GetConfiguration(nameConfigSection);
            config.Validate();
            //TODO: handle exeptions
            var cultureHelper = CultureHelper.Source;
            cultureHelper.SetCurrentCulture(config);
            var watcheHandler = new WatcherHandler(config);
            var watchDirectories = WatcherHelper.GetDirectoriesPath(config.WatchDirectories);
            var watcher = new MultiFileWatcher(new WatcherManager(), watchDirectories);
            var logger = new WatcherListener();

            logger.Subscribe(watcher);
            watcheHandler.Subscribe(watcher);
            watcher.StartWatch();

            var isContinue = true;
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                isContinue = false;
                watcher.StopWatch();
            };
            while (isContinue)
            {
                Console.WriteLine($"{messages.Press_CTRL_C_to_stop}");
                Console.ReadKey();
            }
        }
    }
}
