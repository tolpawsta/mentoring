using BclFileWatcherConsole.Configuration;
using System;
using System.Configuration;

namespace BclFileWatcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationManager.GetSection("appSection") as UserAppCofiguration;
            Console.WriteLine(config.ApplicationName);
            Console.WriteLine("Hello World!");
        }
    }
}
