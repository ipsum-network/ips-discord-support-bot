using System.Collections.Generic;
using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.CoinMarketCap 
{
    public class Currency
    {
        [JsonProperty("data")]
        public Listing CoinData { get; set; }
    }
}