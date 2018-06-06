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
            int BlocksUntilHalve;
            double CurrentBlockReward;
            double DaysUntilHalve;
            //const int test = 70000;
            var result = await this.GetStats();

            EmbedBuilder builder = new EmbedBuilder();

            //if (test < ExplorerStats.firstHalveStart)
            if (result.BlockHeight < ExplorerStats.firstHalveStart)
            {
                //BlocksUntilHalve = ExplorerStats.firstHalveStart - test;
                BlocksUntilHalve = ExplorerStats.firstHalveStart - result.BlockHeight;
                CurrentBlockReward = ExplorerStats.firstHalveReward;
                //DaysUntilHalve = (ExplorerStats.firstHalveStart - test) / ExplorerStats.blocksPerDay;
                DaysUntilHalve = (ExplorerStats.firstHalveStart - result.BlockHeight) / ExplorerStats.blocksPerDay;
                string resultDays = DaysUntilHalve.ToString("0.00");

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                builder.AddInlineField("Blocks Until Next Halve", BlocksUntilHalve);
                builder.AddInlineField("Days left until next halving", resultDays);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);
                builder.AddInlineField("Masternode Rewards", "70%");
                builder.AddInlineField("Current Development Fee", "0%");
                builder.AddInlineField("Staking Rewards", "30%");
            }
            //else if (test < ExplorerStats.secondHalveStart)
            else if (result.BlockHeight < ExplorerStats.secondHalveStart)
            {
                //BlocksUntilHalve = ExplorerStats.secondHalveStart - test;
                BlocksUntilHalve = ExplorerStats.secondHalveStart - result.BlockHeight;
                CurrentBlockReward = ExplorerStats.secondHalveReward;
                //DaysUntilHalve = (ExplorerStats.secondHalveStart - test) / ExplorerStats.blocksPerDay;
                DaysUntilHalve = (ExplorerStats.secondHalveStart - result.BlockHeight) / ExplorerStats.blocksPerDay;
                string resultDays = DaysUntilHalve.ToString("0.00");

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                builder.AddInlineField("Blocks Until Next Halve", BlocksUntilHalve);
                builder.AddInlineField("Days left until next halving", resultDays);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);
                builder.AddInlineField("Masternode Rewards", "70%");
                builder.AddInlineField("Current Development Fee", "0%");
                builder.AddInlineField("Staking Rewards", "30%");
            }
            //else if (test < ExplorerStats.thirdHalveStart)
            else if (result.BlockHeight < ExplorerStats.thirdHalveStart)
            {
                //BlocksUntilHalve = ExplorerStats.thirdHalveStart - test;
                BlocksUntilHalve = ExplorerStats.thirdHalveStart - result.BlockHeight;
                CurrentBlockReward = ExplorerStats.thirdHalveReward;
                //DaysUntilHalve = (ExplorerStats.thirdHalveStart - test) / ExplorerStats.blocksPerDay;
                DaysUntilHalve = (ExplorerStats.thirdHalveStart - result.BlockHeight) / ExplorerStats.blocksPerDay;
                string resultDays = DaysUntilHalve.ToString("0.00");

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                builder.AddInlineField("Blocks Until Next Halve", BlocksUntilHalve);
                builder.AddInlineField("Days left until next halving", resultDays);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);
                builder.AddInlineField("Masternode Rewards", "70%");
                builder.AddInlineField("Current Development Fee", "0%");
                builder.AddInlineField("Staking Rewards", "30%");
            }
            //else if (test < ExplorerStats.fourthHalveStart)
            else if (result.BlockHeight < ExplorerStats.fourthHalveStart)
            {
                //BlocksUntilHalve = ExplorerStats.fourthHalveStart - test;
                BlocksUntilHalve = ExplorerStats.fourthHalveStart - result.BlockHeight;
                CurrentBlockReward = ExplorerStats.thirdHalveReward;
                //DaysUntilHalve = (ExplorerStats.fourthHalveStart - test) / ExplorerStats.blocksPerDay;
                DaysUntilHalve = (ExplorerStats.fourthHalveStart - result.BlockHeight) / ExplorerStats.blocksPerDay;
                string resultDays = DaysUntilHalve.ToString("0.00");

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                builder.AddInlineField("Blocks Until Next Halve", BlocksUntilHalve);
                builder.AddInlineField("Days left until next halving", resultDays);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);                
                builder.AddInlineField("Masternode Rewards", "70%");
                builder.AddInlineField("Current Development Fee", "0%");
                builder.AddInlineField("Staking Rewards", "30%");
            }
            //else if (test > ExplorerStats.fourthHalveStart)
            else if (result.BlockHeight > ExplorerStats.fourthHalveStart)
            {
                //BlocksUntilHalve = test;
                BlocksUntilHalve = result.BlockHeight;
                CurrentBlockReward = ExplorerStats.fourthHalveReward;

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.WithFooter("The halving is over.Thank you for sticking around this long");
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("The Current Block Height", test);
                builder.AddInlineField("The Current Block Height", BlocksUntilHalve);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);
                builder.AddInlineField("Masternode Rewards", "70%");
                builder.AddInlineField("Current Development Fee", "0%");
                builder.AddInlineField("Staking Rewards", "30%");
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
