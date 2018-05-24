namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using DiscordSupportBot.Common;
    using DiscordSupportBot.DAL.Context;
    using DiscordSupportBot.Models.CoinMarketCap;
    using DiscordSupportBot.Models.Exchanges;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class EscrowModule : ModuleBase<SocketCommandContext>
    {
        private DiscordSupportBotDbContext _dbContext { get; set; }
        
        public EscrowModule(DiscordSupportBotDbContext discordSupportBotDbContext)
        {
            this._dbContext = discordSupportBotDbContext;
        }

        [Command("escrow")]
        public async Task Escrow()
        {
            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotTestingChannel)
                .SendMessageAsync((this._dbContext.Database != null).ToString());
        }
    }
}
