namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;
    using DiscordSupportBot.Models.General;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        private string[] VoteOptions = { "1⃣", "2⃣", "3⃣", "4⃣", "5⃣", "6⃣", "7⃣", "8⃣", "9⃣", "🔟" };

        [Command("help")]
        public async Task Help()
        {
            var builder = new EmbedBuilder();

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

        [Command("createpoll")]
        [Alias("poll")]
        public async Task Poll(string question, params string[] options)
        {
            var user = this.Context.Message.Author as SocketGuildUser;
            var permissiveRole = user.Roles.FirstOrDefault(r => r.Name.Equals("devs") || r.Name.Equals("admins"));

            if (permissiveRole != null)
            {
                var builder = new EmbedBuilder();
                var optionsList = this.GetVoteOptions(options);

                builder.WithTitle($"{question.Trim('?')}?")
                    .WithDescription(optionsList)
                    .WithColor(Discord.Color.Blue);

                var message = await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotTestingChannel)
                    .SendMessageAsync(string.Empty, false, builder.Build());

                for (int i = 0; i < options.Length; i++)
                {
                    await message.AddReactionAsync(new Emoji(this.VoteOptions[i]));
                }
            }
            else
            {
                await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotTestingChannel)
                    .SendMessageAsync($"not enough permissions for the command!");
            }
        }

        private string GetVoteOptions(string[] options)
        {
            var result = string.Empty;

            for (int i = 0; i < options.Length; i++)
            {
                result += $"\n{this.VoteOptions[i]} - {options[i]}";
            }

            return result;
        }
    }
}
