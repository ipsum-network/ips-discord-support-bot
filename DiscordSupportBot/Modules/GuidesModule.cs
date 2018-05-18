namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class GuidesModule : ModuleBase<SocketCommandContext>
    {

        [Command("guide")]
        [Alias("guides")]
        public async Task guide()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("Master List of Guides").WithColor(Discord.Color.Blue)
                .WithDescription("\u200b")
                .WithUrl("https://github.com/ipsum-network/guides")
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")

                .AddField("PLEASE UPGRADE TO NEW WALLET VERSION ASAP", "https://github.com/ipsum-network/guides/blob/master/v3.1-UPDATE.md")
                .AddField("Linux Wallet Installation", "https://github.com/ipsum-network/guides/blob/master/LINUX-COLD.md")
                .AddField("Windows Wallet with Linux Masternode VPS", "https://github.com/ipsum-network/guides/blob/master/IPSUM-MN-GUIDE-WINDOWS-LINUX-VPS-SERVER.md")
                .AddField("Configuration Seed List", "https://github.com/ipsum-network/seeds");

            await this.ReplyAsync(string.Empty, false, builder.Build());
        }
    }
}
