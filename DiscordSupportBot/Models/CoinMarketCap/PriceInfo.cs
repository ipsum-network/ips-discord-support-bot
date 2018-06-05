using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.CoinMarketCap 
{
    public class PriceInfo
    {
        [JsonProperty("price")]
        public float Price { get; set; }
        [JsonProperty("volume_24h")]
        public float Volume { get; set; }
        [JsonProperty("percent_change_24h")]
        public float PercentChange24h { get; set; }
    }
}