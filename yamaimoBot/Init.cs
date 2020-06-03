using System;
using System.Timers;

namespace yamaimoBot
{
    class Init
    {

        static void Main(string[] args)
        {
            Bot bot = new Bot();

            bot.GetContent();

            _ = Console.ReadKey();
        }
    }
}
