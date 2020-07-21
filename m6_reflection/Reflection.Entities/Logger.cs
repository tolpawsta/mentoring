using System;
using Reflection.DIContainer.Attributes;

namespace Reflection.Entities
{
    [Export]
    public class Logger
    {
        [Import] 
        private string Name { get; set; }
        public void InfoToConsole(string message)
        {
            Console.WriteLine($"[{DateTimeOffset.Now:d}] {message}");
        }
    }
}