using IntroductionCore;
using static System.Console;

namespace IntroductionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter username: ");
            string userName = ReadLine();
            WriteLine(MessageSender.SendHelloToUser(userName));
        }
    }
}
