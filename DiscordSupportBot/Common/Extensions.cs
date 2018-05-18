using System;
using System.Threading.Tasks;


namespace DiscordSupportBot.Common
{
    public static class Extensions
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static DateTime ParseEpochToDateTime(this int unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }
    }
}

