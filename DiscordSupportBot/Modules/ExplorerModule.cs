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
<<<<<<< HEAD
            // const int test = 140000;
=======
           // const int test = 140000;
>>>>>>> bc29b685359f585a61856d95b33d0167f6229b9c
            var result = await this.GetStats();

            EmbedBuilder builder = new EmbedBuilder();

            if (result.BlockHeight < BlockStats.firstHalveStart)
            {
                BlocksUntilHalve = BlockStats.firstHalveStart - result.BlockHeight;
                CurrentBlockReward = BlockStats.currentReward;

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                builder.AddInlineField("Blocks Until Next Halve", BlocksUntilHalve);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);
                builder.AddInlineField("Days left until next halving", (BlockStats.firstHalveStart - result.BlockHeight) / BlockStats.blocksPerDay * 100 /100);
            }
            else if (result.BlockHeight < BlockStats.secondHalveStart)
            {
                BlocksUntilHalve = BlockStats.secondHalveStart - result.BlockHeight;
                CurrentBlockReward = BlockStats.firstHalveReward;

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                builder.AddInlineField("Blocks Until Next Halve", BlocksUntilHalve);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);
                // builder.AddInlineField("Days left until next halving", (BlockStats.secondHalveStart - test) / BlockStats.blocksPerDay * 100 /100);
                builder.AddInlineField("Days left until next halving", (BlockStats.secondHalveStart - result.BlockHeight) / BlockStats.blocksPerDay * 100 / 100);
            }
            else if (result.BlockHeight < BlockStats.thirdHalveStart)
            {
                BlocksUntilHalve = BlockStats.thirdHalveStart - result.BlockHeight;
                CurrentBlockReward = BlockStats.secondHalveReward;

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
               // builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                builder.AddInlineField("Blocks Until Next Halve", BlocksUntilHalve);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);
                // builder.AddInlineField("Days left until next halving", (BlockStats.thirdHalveStart - test) / BlockStats.blocksPerDay * 100 /100);
                builder.AddInlineField("Days left until next halving", (BlockStats.thirdHalveStart - result.BlockHeight) / BlockStats.blocksPerDay * 100 / 100);
            }
            else if (result.BlockHeight < BlockStats.fourthHalveStart)
            {
                BlocksUntilHalve = BlockStats.fourthHalveStart - result.BlockHeight;
                CurrentBlockReward = BlockStats.thirdHalveReward;

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                //builder.AddInlineField("Current Block Height", test);
                builder.AddInlineField("Current Block Height", result.BlockHeight);
                builder.AddInlineField("Blocks Until Next Halve", BlocksUntilHalve);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);
                // builder.AddInlineField("Days left until next halving", (BlockStats.fourthHalveStart - test) / BlockStats.blocksPerDay * 100 /100);
                builder.AddInlineField("Days left until next halving", (BlockStats.fourthHalveStart - result.BlockHeight) / BlockStats.blocksPerDay * 100 / 100);
            }
            else if (result.BlockHeight > BlockStats.fourthHalveStart)
            {
                BlocksUntilHalve = result.BlockHeight;
                CurrentBlockReward = BlockStats.fourthHalveReward;

                builder.WithTitle("Stats").WithColor(Discord.Color.Blue);
                builder.AddInlineField("Difficulty", result.Difficulty);
                builder.AddInlineField("Masternodes Count", result.MasternodeCount);
                builder.AddInlineField("The Current Block Height", BlocksUntilHalve);
                builder.AddInlineField("Current Reward Per Block", CurrentBlockReward);
                builder.AddInlineField("The halving is over. Thank you for sticking around this long.", "");
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

        private async Task<ExplorerStatsResponse> GetStats()
        {
            var difficultyResponse = await client.GetAsync($"https://explorer.ipsum.network/api/getdifficulty");
            var masternodeResponse = await client.GetAsync($"https://explorer.ipsum.network/api/getmasternodecount");
            var supplyResponse = await client.GetAsync($"https://explorer.ipsum.network/api/getsupply");
            var blockResponse = await client.GetAsync($"https://explorer.ipsum.network/api/getblockcount");

            var result = new ExplorerStatsResponse
            {
                Difficulty = float.Parse(difficultyResponse.Content.ReadAsStringAsync().Result),
                BlockHeight = int.Parse(blockResponse.Content.ReadAsStringAsync().Result),
                MasternodeCount = int.Parse(JsonConvert.DeserializeObject<dynamic>(masternodeResponse.Content.ReadAsStringAsync().Result).total.Value.ToString())
            };

            return result;
        }

        public class ExplorerStatsResponse
        {
            public float Difficulty { get; set; }
            public int MasternodeCount { get; set; }
            public int BlockHeight { get; set; }
        }

        public enum StatsDataType
        {
            Difficulty,
            MasternodeCount,
            BlockHeight
        }
    }
}
