namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class CoinInfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("ipsum")][Alias("ips")]
        public async Task Ipsum()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Ipsum [IPS]")
                .WithDescription("\u200b")
                .WithFooter("https://ipsum.network/")
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")
                .WithColor(Discord.Color.Blue);

            // builder.AddField("Specifications:", "\u200b");
            // builder.AddInlineField("Algorhitm", "x11");
            // builder.AddInlineField("Coin supply", "21000k");

            await this.ReplyAsync(string.Empty, false, builder.Build());
        }
    }
}
