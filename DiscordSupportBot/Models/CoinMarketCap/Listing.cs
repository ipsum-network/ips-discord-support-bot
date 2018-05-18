using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.CoinMarketCap 
{
    public class Listing
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("website_slug")]
        public string WebSlug { get; set; }

        [JsonProperty("quotes")]
        public Quotes Quotes { get; set; }

        [JsonProperty("last_updated")]
        public int LastUpdate { get; set; }
    }
}