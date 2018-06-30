using DiscordSupportBot.Models.BaseModels;
using DiscordSupportBot.Modules;
using Newtonsoft.Json;

namespace DiscordSupportBot.Models.Explorer
{
    public class ExplorerStats
    {
        public float Difficulty { get; set; }

        public int MasternodeCount { get; set; }

        public int BlockHeight { get; set; }
    }
}