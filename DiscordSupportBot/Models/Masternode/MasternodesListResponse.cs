using System.Collections.Generic;
using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.Masternode
{
    public class MasternodesListResponse : ResponseBase
    {
        [JsonProperty("Result")]
        public List<Masternode> Masternodes { get; set; }
    }
}