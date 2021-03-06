using System;
using System.Globalization;
using System.Threading.Tasks;


namespace DiscordSupportBot.Common.Extensions
{
    public static class Extensions
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string ParseEpochToDateTimeLastPaid(this int unixTime)
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

        public static DateTime ParseEpochToDateTime(this int unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }

        public static string DecimalToString(this decimal dec)
        {
            var strdec = dec.ToString(CultureInfo.InvariantCulture);

            return strdec.Contains(".") ? strdec.TrimEnd('0').TrimEnd('.') : strdec;
        }
    }
}

