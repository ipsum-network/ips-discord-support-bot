using System;
using System.Collections.Generic;
using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.CoinMarketCap 
{
    public class Quotes
    {
        [JsonProperty("USD")]
        public PriceInfo PriceUSD { get; set; }

        [JsonProperty("BTC")]
        public PriceInfo PriceBTC { get; set; }
    }
}