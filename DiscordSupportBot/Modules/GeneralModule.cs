namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Help()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Ipsum Bot Help")
                .WithColor(Discord.Color.Blue)
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")
                .WithFooter("https://ipsum.network/")

                .AddField("//help", "shows available commands")
                .AddField("//ipsum or //ips", "shows coin info")
                .AddField("//guides or //guide", "replies with current installation guides")
                .AddField("//mnstatus <pubkey> or //masternode <pubkey>", "checks the status of your masternode")
                .AddField("//price <ticker> or //checkprice <ticker>", "replies with cmc price");

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotTestingChannel)
                .SendMessageAsync($"{this.Context.Message.Author.Mention}", false, builder.Build());
        }
    }
}
