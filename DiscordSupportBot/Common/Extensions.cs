using System;
using System.Threading.Tasks;


namespace DiscordSupportBot.Common
{
    public static class Extensions
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static string ParseEpochToDateTime(this int unixTime)
        {
            if (unixTime != 0)
            {
                return epoch.AddSeconds(unixTime).ToString();
            }
            else
            {
                return "Not paid yet!";
            }
        }
    }
}

