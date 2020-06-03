using System;
using System.Timers;

namespace yamaimoBot
{
    class Init
    {

        static void Main(string[] args)
        {
            Bot bot = new Bot();
            Timer timer = new Timer(60000);

            timer.Elapsed += (s, e) => { bot.GetContent(); };

            timer.Start();

            _ = Console.ReadKey();
        }
    }
}
