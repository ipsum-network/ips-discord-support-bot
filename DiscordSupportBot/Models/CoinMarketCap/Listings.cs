using System;
using System.Collections.Generic;
using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.CoinMarketCap 
{
    public class Listings : ResponseBase
    {
        [JsonProperty("data")]
        public List<Listing> Currencies { get; set; }

        public DateTime LastUpdatedFromApi => DateTime.Now;
    }
}