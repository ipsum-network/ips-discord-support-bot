namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class DonationModule : ModuleBase<SocketCommandContext>
    {

        [Command("donate")]
        [Alias("donations")]
        public async Task Donation()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("").WithColor(Discord.Color.Blue)
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")

                //Provided by 0x1
                .AddField("Donations will be used for:", "Exchange Listings, Development, and Infrastructure")
                //Dev IPS wallet provided by 0x1
                .AddField("IPS Donation Address:", "iSv6vXhSbb7WH8D3dVHuWecZ7pGj4AJMmt")
                //Dev BTC wallet provided by 0x1
                .AddField("BTC Donation Address:", "1592K4xS5QkXDStELPk9nHBEqZ5vLNAyrm");
                
            await this.ReplyAsync(string.Empty, false, builder.Build());
        }
    }
}
