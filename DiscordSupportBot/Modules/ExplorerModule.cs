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
    using System.Threading.Tasks;
    using System.Net.Http;

    public class ExplorerModule : ModuleBase<SocketCommandContext>
    {
        private static HttpClient client = new HttpClient();
       
        [Command("stats")]
        public async Task Stats()
        {
            
            double DaysUntilHalve;
            //const int test = 70000;
            var result = await this.GetStats();

            EmbedBuilder builder = new EmbedBuilder();

            //if (test < ExplorerStats.firstHalveStart)
            if (result.BlockHeight < ExplorerStats.firstHalveStart)
            {
                //DaysUntilHalve = (ExplorerStats.firstHalveStart - test) / ExplorerStats.blocksPerDay;
                DaysUntilHalve = (ExplorerStats.firstHalveStart - result.BlockHeight) / ExplorerStats.blocksPerDay;
                //string resultDays = DaysUntilHalve.ToString("0.00");
                TimeSpan timespan = TimeSpan.FromDays(DaysUntilHalve);
                string resultDays = timespan.ToString("d\\:hh");

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.WithFooter("All block rewards are calculated on a 70% Masternode 30% Staking 0% Development Fee");
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                //builder.AddInlineField("Blocks Until Next Halve", ExplorerStats.firstHalveStart - test);
                builder.AddInlineField("Blocks Until Next Halve", ExplorerStats.firstHalveStart - result.BlockHeight);
                builder.AddInlineField("Days:Hours until next halving", resultDays);
                builder.AddInlineField("Current Reward Per Block", ExplorerStats.currentReward);
                builder.AddInlineField("Masternode Rewards",(ExplorerStats.currentReward * ExplorerStats.MasternodeReward));
                builder.AddInlineField("Current Development", (ExplorerStats.currentReward * ExplorerStats.DevelopmentFee));
                builder.AddInlineField("Staking Rewards", (ExplorerStats.currentReward * ExplorerStats.StakingReward));
            }
            //else if (test < ExplorerStats.secondHalveStart)
            else if (result.BlockHeight < ExplorerStats.secondHalveStart)
            {
                //DaysUntilHalve = (ExplorerStats.secondHalveStart - test) / ExplorerStats.blocksPerDay;
                DaysUntilHalve = (ExplorerStats.secondHalveStart - result.BlockHeight) / ExplorerStats.blocksPerDay;
                //string resultDays = DaysUntilHalve.ToString("0.00");
                TimeSpan timespan = TimeSpan.FromDays(DaysUntilHalve);
                string resultDays = timespan.ToString("d\\:hh");

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.WithFooter("All block rewards are calculated on a 70% Masternode 30% Staking 0% Development Fee");
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                //builder.AddInlineField("Blocks Until Next Halve", ExplorerStats.secondHalveStart - test);
                builder.AddInlineField("Blocks Until Next Halve", ExplorerStats.secondHalveStart - result.BlockHeight);
                builder.AddInlineField("Days:Hours until next halving", resultDays);
                builder.AddInlineField("Current Reward Per Block", ExplorerStats.firstHalveReward);
                builder.AddInlineField("Masternode Rewards", (ExplorerStats.firstHalveReward * ExplorerStats.MasternodeReward));
                builder.AddInlineField("Current Development", (ExplorerStats.firstHalveReward * ExplorerStats.DevelopmentFee));
                builder.AddInlineField("Staking Rewards", (ExplorerStats.firstHalveReward * ExplorerStats.StakingReward));
            }
            //else if (test < ExplorerStats.thirdHalveStart)
            else if (result.BlockHeight < ExplorerStats.thirdHalveStart)
            {
                //DaysUntilHalve = (ExplorerStats.thirdHalveStart - test) / ExplorerStats.blocksPerDay;
                DaysUntilHalve = (ExplorerStats.thirdHalveStart - result.BlockHeight) / ExplorerStats.blocksPerDay;
                //string resultDays = DaysUntilHalve.ToString("0.00");
                TimeSpan timespan = TimeSpan.FromDays(DaysUntilHalve);
                string resultDays = timespan.ToString("d\\:hh");

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.WithFooter("All block rewards are calculated on a 70% Masternode 30% Staking 0% Development Fee");
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                //builder.AddInlineField("Blocks Until Next Halve", ExplorerStats.thirdHalveStart - test);
                builder.AddInlineField("Blocks Until Next Halve", ExplorerStats.thirdHalveStart - result.BlockHeight);
                builder.AddInlineField("Days:Hours until next halving", resultDays);
                builder.AddInlineField("Current Reward Per Block", ExplorerStats.secondHalveReward);
                builder.AddInlineField("Masternode Rewards", (ExplorerStats.secondHalveReward * ExplorerStats.MasternodeReward));
                builder.AddInlineField("Current Development", (ExplorerStats.secondHalveReward * ExplorerStats.DevelopmentFee));
                builder.AddInlineField("Staking Rewards", (ExplorerStats.secondHalveReward * ExplorerStats.StakingReward));
            }
            //else if (test < ExplorerStats.fourthHalveStart)
            else if (result.BlockHeight < ExplorerStats.fourthHalveStart)
            {
                //DaysUntilHalve = (ExplorerStats.fourthHalveStart - test) / ExplorerStats.blocksPerDay;
                DaysUntilHalve = (ExplorerStats.fourthHalveStart - result.BlockHeight) / ExplorerStats.blocksPerDay;
                //string resultDays = DaysUntilHalve.ToString("0.00");
                TimeSpan timespan = TimeSpan.FromDays(DaysUntilHalve);
                string resultDays = timespan.ToString("d\\:hh");

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.WithFooter("All block rewards are calculated on a 70% Masternode 30% Staking 0% Development Fee");
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                //builder.AddInlineField("Blocks Until Next Halve", ExplorerStats.fourthHalveStart - test);
                builder.AddInlineField("Blocks Until Next Halve", ExplorerStats.fourthHalveStart - result.BlockHeight);
                builder.AddInlineField("Days:Hours until next halving", resultDays);
                builder.AddInlineField("Current Reward Per Block", ExplorerStats.thirdHalveReward);
                builder.AddInlineField("Masternode Rewards", (ExplorerStats.thirdHalveReward * ExplorerStats.MasternodeReward));
                builder.AddInlineField("Current Development", (ExplorerStats.thirdHalveReward * ExplorerStats.DevelopmentFee));
                builder.AddInlineField("Staking Rewards", (ExplorerStats.thirdHalveReward * ExplorerStats.StakingReward));
            }
            //else if (test > ExplorerStats.fourthHalveStart)
            else if (result.BlockHeight > ExplorerStats.fourthHalveStart)
            {
                                                
                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.WithFooter("All block rewards are calculated on a 70% Masternode 30% Staking 0% Development Fee");
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("The Current Block Height", test);
                builder.AddInlineField("The Current Block Height", result.BlockHeight);
                builder.AddInlineField("Current Reward Per Block", ExplorerStats.fourthHalveReward);
                builder.AddInlineField("Masternode Rewards", (ExplorerStats.fourthHalveReward * ExplorerStats.MasternodeReward));
                builder.AddInlineField("Current Development", (ExplorerStats.fourthHalveReward * ExplorerStats.DevelopmentFee));
                builder.AddInlineField("Staking Rewards", (ExplorerStats.fourthHalveReward * ExplorerStats.StakingReward));
            }

            
            var isBotChannel = this.Context.Channel.Id.Equals(DiscordSupportBot.Common.DiscordData.BotChannel);


            await this.ReplyAsync(string.Empty, false, builder.Build());
           // var isBotChannel = this.Context.Channel.Id.Equals(DiscordSupportBot.Common.DiscordData.BotChannel);

           // await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotChannel)
              //  .SendMessageAsync(isBotChannel ? string.Empty : this.Context.Message.Author.Mention, false, builder.Build());
        }

        [Command("balance")]
        public async Task Balance(string address)
        {
            var result = await this.GetAddressBalance(address);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Ipsum Bot Balance").WithColor(Discord.Color.Blue);
            builder.WithDescription(result.ToString() + " IPS");

            await this.Context.Message.Author.SendMessageAsync(string.Empty, false, builder.Build());
        }

        private async Task<string> GetAddressBalance(string address)
        {
            HttpResponseMessage response = await client.GetAsync($"https://explorer.ipsum.network/ext/getbalance/{address}");
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
