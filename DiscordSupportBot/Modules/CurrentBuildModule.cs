namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class CurrentBuildModule : ModuleBase<SocketCommandContext>
    {

        [Command("build")]
        [Alias("version")]
        public async Task CurrentBuild()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("The current build is on 3.1.0").WithColor(Discord.Color.Blue)
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")
                .WithDescription("\u200b")

                .AddField("Please update your wallets and masternodes!", "https://github.com/ipsum-network/ips/releases");
                           
            await this.ReplyAsync(string.Empty, false, builder.Build());
        }
    }
}
