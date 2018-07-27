using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordSupportBot.Common.Constants;

namespace DiscordSupportBot.Common.Extensions
{
    public static class DiscordExtensions
    {
        public static Discord.Color WhiteColor { get => new Discord.Color(255, 255, 255); }

        public static async Task SetGameAsPrice(this DiscordSocketClient client)
        {
            await client.SetGameAsync("//help");
        }

        public static async Task SendMessageViaContext(this SocketCommandContext context, string resultString)
        {
            if (context.IsPrivate)
            {
                await context.Message.Author.SendMessageAsync(resultString);
            }
            else
            {
                var isBotChannel = context.Channel.Id.Equals(DiscordDataConstants.BotChannel);

                await context.Guild.GetTextChannel(DiscordDataConstants.BotChannel)
                    .SendMessageAsync($"{(isBotChannel ? resultString : $"{context.Message.Author.Mention} {resultString}")}");
            }
        }

        public static async Task SendEmbedMessageViaContext(this SocketCommandContext context, Embed msg)
        {
            if (context.IsPrivate)
            {
                await context.Message.Author.SendMessageAsync(string.Empty, false, msg);
            }
            else
            {
                var isBotChannel = context.Channel.Id.Equals(DiscordDataConstants.BotChannel);

                await context.Guild.GetTextChannel(DiscordDataConstants.BotChannel)
                    .SendMessageAsync(isBotChannel ? string.Empty : context.Message.Author.Mention, false, msg);
            }
        }
    }
}