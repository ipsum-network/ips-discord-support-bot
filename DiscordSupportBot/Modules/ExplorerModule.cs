namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DiscordSupportBot.Models.Explorer;
    using DiscordSupportBot.Common.Constants;
    using System.Threading.Tasks;
    using System.Net.Http;
    using DiscordSupportBot.Common.Extensions;

    public class ExplorerModule : ModuleBase<SocketCommandContext>
    {
        private static HttpClient client = new HttpClient();

        [Command("stats")]
        public async Task Stats()
        {
            double daysUntilHalve = 0;
            var result = await this.GetStats();

            EmbedBuilder builder = new EmbedBuilder();

            if (result.BlockHeight < ExplorerModuleConstants.firstHalveStart)
            {
                daysUntilHalve = (ExplorerModuleConstants.firstHalveStart - result.BlockHeight) / ExplorerModuleConstants.blocksPerDay;
            }
            else if (result.BlockHeight < ExplorerModuleConstants.secondHalveStart)
            {
                daysUntilHalve = (ExplorerModuleConstants.secondHalveStart - result.BlockHeight) / ExplorerModuleConstants.blocksPerDay;
            }
            else if (result.BlockHeight < ExplorerModuleConstants.thirdHalveStart)
            {
                daysUntilHalve = (ExplorerModuleConstants.thirdHalveStart - result.BlockHeight) / ExplorerModuleConstants.blocksPerDay;
            }
            else if (result.BlockHeight < ExplorerModuleConstants.fourthHalveStart)
            {
                daysUntilHalve = (ExplorerModuleConstants.fourthHalveStart - result.BlockHeight) / ExplorerModuleConstants.blocksPerDay;
            }

            builder.WithTitle("Stats").WithColor(Color.Blue);
            builder.WithFooter("All block rewards are split: 70% Masternode, 30% Staking and 0% Development fee");
            builder.AddInlineField("Difficulty", result.Difficulty);
            builder.AddInlineField("Masternodes Count", result.MasternodeCount);
            builder.AddInlineField("Current Block Height", result.BlockHeight);

            if (result.BlockHeight < ExplorerModuleConstants.fourthHalveStart)
            {
                TimeSpan timespan = TimeSpan.FromDays(daysUntilHalve);
                string resultDays = timespan.ToString("d\\:hh");

                builder.AddInlineField("Blocks Until Next Halve", ExplorerModuleConstants.firstHalveStart - result.BlockHeight);
                builder.AddInlineField("Days:Hours until next halving", resultDays);
            }

            builder.AddInlineField("Current Reward Per Block", ExplorerModuleConstants.currentReward);
            builder.AddInlineField("Masternode Rewards", (ExplorerModuleConstants.currentReward * ExplorerModuleConstants.MasternodeReward));
            builder.AddInlineField("Current Development Fee", (ExplorerModuleConstants.currentReward * ExplorerModuleConstants.DevelopmentFee));
            builder.AddInlineField("Staking Rewards", (ExplorerModuleConstants.currentReward * ExplorerModuleConstants.StakingReward));

            await this.Context.SendEmbedMessageViaContext(builder.Build());
        }

        [Command("balance")]
        public async Task Balance(string address)
        {
            var result = await this.GetAddressBalance(address);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Ipsum Bot Balance").WithColor(Color.Blue);
            builder.WithDescription(result.ToString() + " IPS");

            await this.Context.Message.Author.SendMessageAsync(string.Empty, false, builder.Build());
        }

        private async Task<string> GetAddressBalance(string address)
        {
            var response = await client.GetAsync($"https://explorer.ipsum.network/ext/getbalance/{address}");
            var result = response.Content.ReadAsStringAsync();

            return result.Result;
        }

        private async Task<ExplorerStats> GetStats()
        {
            var difficultyResponse = await client.GetAsync($"https://explorer.ipsum.network/api/getdifficulty");
            var masternodeResponse = await client.GetAsync($"https://explorer.ipsum.network/api/getmasternodecount");
            var supplyResponse = await client.GetAsync($"https://explorer.ipsum.network/api/getsupply");
            var blockResponse = await client.GetAsync($"https://explorer.ipsum.network/api/getblockcount");

            var result = new ExplorerStats
            {
                Difficulty = float.Parse(difficultyResponse.Content.ReadAsStringAsync().Result),
                BlockHeight = int.Parse(blockResponse.Content.ReadAsStringAsync().Result),
                MasternodeCount = int.Parse(JsonConvert.DeserializeObject<dynamic>(masternodeResponse.Content.ReadAsStringAsync().Result).total.Value.ToString())
            };

            return result;
        }
    }
}
