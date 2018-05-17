namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Http;

    public class CoinPriceModule : ModuleBase<SocketCommandContext>
    {
        private static HttpClient client = new HttpClient();

        [Command("price")]
        public async Task Price()
        {
            var result = await this.GetPriceStats();

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Current Price on Graviex").WithColor(Discord.Color.Blue);
            builder.AddInlineField("Price", result.CurrentPrice);
            
            await this.ReplyAsync(string.Empty, false, builder.Build());
        }


        private async Task<ExplorerStatsResponse> GetPriceStats()
        {
            var priceResponse = await client.GetAsync($"https://graviex.net:443//api/v2/tickers/ipsbtc.json");
   
            var result = new ExplorerStatsResponse
            {
                CurrentPrice = float.Parse(priceResponse.Content.ReadAsStringAsync().Result),
            };

            return result;
        }

        public class ExplorerStatsResponse
        {
            public float CurrentPrice { get; set; }
        }

        public enum StatsDataType
        {
            CurrentPrice
        }
    }
}
