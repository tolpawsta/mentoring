using BclFileWatcherConsole.Configuration;
using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using messages = BclFileWatcherConsole.Resources.Messages;
namespace BclFileWatcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var config = AppConfigManager.GetConfiguration("appSection");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(config.Culture.Name);
                Console.WriteLine(config.ApplicationName);
                Console.WriteLine(messages.All_right);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message,ex.StackTrace);
            }
        }
    }
}
