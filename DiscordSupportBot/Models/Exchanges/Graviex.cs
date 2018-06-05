using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.Exchanges 
{
    public class Graviex : ResponseBase
    {
        [JsonProperty("at")]
        public int TimeOfUpdate { get; set; }
        [JsonProperty("ticker")]
        public GraviexTicker Ticker { get; set; }
    }

    public class GraviexTicker
    {
        [JsonProperty("last")]
        public string Last { get; set; }

        [JsonProperty("volbtc")]
        public string VolumeBtc { get; set; }
    }
}