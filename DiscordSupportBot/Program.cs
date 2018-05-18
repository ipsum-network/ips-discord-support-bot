namespace DiscordSupportBot
{
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using DiscordSupportBot.Common;

    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient Client { get; set; }
        private CommandService Commands { get; set; }
        private IServiceProvider Services { get; set; }

        public async Task RunBotAsync()
        {
            this.Client = new DiscordSocketClient();
            this.Commands = new CommandService();

            this.Services = new ServiceCollection()
                .AddSingleton(this.Client)
                .AddSingleton(this.Commands)
                .BuildServiceProvider();

            var botToken = "";

            this.Client.Log += Log;
            this.Client.MessageReceived += HandleCommandAsync;

            await RegisterCommandsAsync();

            await this.Client.LoginAsync(TokenType.Bot, botToken);

            await this.Client.StartAsync();

            await this.Client.SetGameAsPrice();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(DateTime.Now + " " + msg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            await this.Commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;

            if (msg == null || msg.Author.IsBot)
            {
                return;
            }

            var argPos = 0;

            if (msg.HasStringPrefix("//", ref argPos) || msg.HasMentionPrefix(this.Client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(this.Client, msg);
                var result = await this.Commands.ExecuteAsync(context, argPos, this.Services);

                if (!result.IsSuccess)
                {
                    await msg.Channel.SendMessageAsync($"{msg.Author.Mention} Sorry dude, something went wrong, particularly: {result.ErrorReason}");
                }
            }
        }
    }
}
