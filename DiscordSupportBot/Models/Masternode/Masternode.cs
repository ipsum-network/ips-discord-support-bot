using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.Masternode 
{
    public class Masternode
    {
        public int Rank { get; set; }
        public string TxHash { get; set; }
        public int OutIdx { get; set; }
        public string Status { get; set; }
        
        [JsonProperty("Addr")]
        public string Address { get; set; }
        public int Version { get; set; }
        public int LastSeen { get; set; }
        public int ActiveTime { get; set; }
        public int LastPaid { get; set; }
    }
}