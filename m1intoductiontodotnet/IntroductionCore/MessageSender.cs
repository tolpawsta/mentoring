using System;

namespace IntroductionCore
{
    public static class MessageSender
    {
        public static string SendHelloToUser(string userName)
        {
            DateTime currentTime = DateTime.Now;
            return $"{currentTime:HH:mm:ss} Hello, {userName}!";
        }
    }
}
