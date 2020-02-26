using System;

namespace ClassLibrary
{
    public static class Greeting
    {
        public static string GetGreeting(string userName)
            => $"{DateTime.Now} Hello, {userName}!";
    }
}
