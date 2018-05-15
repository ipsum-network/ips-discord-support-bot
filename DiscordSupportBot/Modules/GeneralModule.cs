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
                .WithColor(Discord.Color.Blue);
            builder.AddField("//help", "shows available commands");
            builder.AddField("//ipsum or //ips", "shows coin info");
            builder.AddField("//page or //website", "replies with website link");

            await this.ReplyAsync(string.Empty, false, builder.Build());
        }

        [Command("website")][Alias("page")]
        public async Task WebSite()
        {
            await this.ReplyAsync($"{this.Context.Message.Author.Mention} https://ipsum.network", false);
        }
    }
}
