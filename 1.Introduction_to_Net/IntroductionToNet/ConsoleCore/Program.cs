using System;
using System.Collections.Generic;
using ClassLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;

namespace IntroductionToNet
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Greeting.GetGreeting(args[0]));
            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args, new Dictionary<string, string>
            {
                ["-Name"] = "Name"
            });

            var config = builder.Build();

            var name = config["Name"];

            Console.WriteLine($"Hello, {name}!");
        }
    }
}