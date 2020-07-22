using Reflection.DIContainer.Interfaces;
namespace Reflection.Console
{
    public class ConsoleMessageWriter : IMessageWriter
    {
        public void WriteLine(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}