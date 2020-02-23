using System;

namespace ClassLibrary
{
    public static class Class1
    {
        public static string GetGreeting(string userName)
            => $"{DateTime.Now} Hello, {userName}!";
    }
}
