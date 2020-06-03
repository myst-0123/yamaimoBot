using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using CoreTweet;
using CoreTweet.Streaming;

namespace yamaimoBot
{
    class Bot
    {
        private static Tokens tokens;
        private static string[] tags = { "#よかったやまいも", "#よくないさといも", "#おこったじゃがいも" };

        public Bot()
        {
            var consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            var consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
            var accessToken = ConfigurationManager.AppSettings["accessToken"];
            var accessSecret = ConfigurationManager.AppSettings["accessSecret"];

            tokens = Tokens.Create(consumerKey, consumerSecret, accessToken, accessSecret);

            GetContent();
        }

        public void GetContent()
        {
            foreach (var m in tokens.Streaming.Filter(track: string.Join(",", tags))
                .OfType<StatusMessage>()
                .Select(x => x.Status))
                    Reaction(m);
        }

        private void Reaction(Status status)
        {
            try
            {
                if (!IsRetweet(status) && !IsTweetedMe(status) && !(bool)status.IsFavorited)
                {
                    Console.WriteLine(status.User.Name);
                    Console.WriteLine(status.Text);
                    Console.WriteLine("------------------------------------");
                    tokens.Favorites.Create(status.Id);
                    status.IsFavorited = true;
                    tokens.Statuses.Retweet(status.Id);
                    status.IsRetweeted = true;
                }
            }
            catch (Exception e) { Console.WriteLine("Error: " + e.Message); }
        }

        private bool IsTweetedMe(Status status)
        {
            if (status.User.ScreenName == "yamaimoBot") return true;
            else return false;
        }

        private bool IsRetweet(Status status)
        {
            if (status.Text.Length < 2) return false;

            if (status.Text.Substring(0, 2) == "RT") return true;
            else return false;
        }
    }
}
