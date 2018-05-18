namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using DiscordSupportBot.Common;
    using DiscordSupportBot.Models.Exchanges;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class CoinInfoModule : ModuleBase<SocketCommandContext>
    {
        private static HttpClient client = new HttpClient();

        [Command("ipsum")][Alias("ips")]
        public async Task Ipsum()
        {
            var data = this.GetGraviexData();

            var builder = new EmbedBuilder();

            builder.WithTitle("Ipsum [IPS]")
                .WithDescription("\u200b")
                .WithCurrentTimestamp()
                .WithFooter("https://ipsum.network/")
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")
                .WithColor(Discord.Color.Blue)
                 
                .AddInlineField("Time", $"{(data.Result.Success ? data.Result.TimeOfUpdate.ParseEpochToDateTime().ToString() : "couldn't get the time of retrival")}")
                .AddInlineField("Price", $"{(data.Result.Success ? data.Result.Ticker.Last.ToString() : "couldn't get the price")}")
                .AddInlineField("Volume BTC", $"{(data.Result.Success ? data.Result.Ticker.VolumeBtc.ToString() : "couldn't get the volume")}");

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotTestingChannel)
                .SendMessageAsync($"{this.Context.Message.Author.Mention}", false, builder.Build());
        }

        public async Task<Graviex> GetGraviexData()
        {
            var response = await client.GetStringAsync($"https://graviex.net:443//api/v2/tickers/ipsbtc.json");
            var result = JsonConvert.DeserializeObject<Graviex>(response.ToString());

            return result;
        }
    }
}
