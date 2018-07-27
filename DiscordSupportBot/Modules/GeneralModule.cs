namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;
    using DiscordSupportBot.Common;
    using DiscordSupportBot.Common.Constants;
    using DiscordSupportBot.Common.Extensions;
    using DiscordSupportBot.Models.Exchanges;
    using DiscordSupportBot.Models.Github;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        private readonly string[] VoteOptions = { "1⃣", "2⃣", "3⃣", "4⃣", "5⃣", "6⃣", "7⃣", "8⃣", "9⃣", "🔟" };
        private static HttpClient client = new HttpClient();

        [Command("help")]
        public async Task Help()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("Ipsum Bot Help")
                .WithColor(Color.Blue)
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")
                .WithFooter("https://ipsum.network/")

                .AddField("//help", "shows available commands")
                .AddField("//ipsum or //ips", "shows coin info")
                .AddField("//guides or //guide", "replies with current installation guides")
                .AddField("//mnstatus <pubkey> or //masternode <pubkey>", "checks the status of your masternode")
                .AddField("//mnconnect <ip:port>", "checks the connection status of your masternode")
                .AddField("//price <ticker> or //checkprice <ticker>", "replies with cmc price")
                .AddField("//donate or //donations", "replies with IPS, BTC donation address and balances")
                .AddField("//build", "replies with current wallet and masternode build");

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordDataConstants.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordDataConstants.BotChannel)
                .SendMessageAsync(isBotChannel ? string.Empty : this.Context.Message.Author.Mention, false, builder.Build());
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
                    .WithColor(Color.Blue);

                var message = await this.Context.Guild.GetTextChannel(DiscordDataConstants.BotPollChannel)
                    .SendMessageAsync(string.Empty, false, builder.Build());

                for (int i = 0; i < options.Length; i++)
                {
                    await message.AddReactionAsync(new Emoji(this.VoteOptions[i]));
                }
            }
            else
            {
                await this.Context.Message.Author.SendMessageAsync($"not enough permissions for the usage of poll command!");
            }
        }

        [Command("ipsum")]
        [Alias("ips")]
        public async Task Ipsum()
        {
            var crexData = await this.GetCrexData();
            var graviexData = await this.GetGraviexData();

            var builder = new EmbedBuilder();

            builder.WithTitle("Comparison of Crex24 and Graviex Prices / Volumes")
                .WithCurrentTimestamp()
                .WithFooter("https://ipsum.network/")
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")
                .WithColor(Color.Blue)
                .AddField("\u200b", "Crex")
                .AddInlineField("Price", $"{crexData.Tickers[0].CrexLast.DecimalToString()}")
                .AddInlineField("24h Volume BTC", $"{crexData.Tickers[0].CrexVolumeInBtc.DecimalToString()}")
                .AddInlineField("24h Volume USD", $"${crexData.Tickers[0].CrexVolumeInUsd.DecimalToString()}")
                .AddInlineField("24h BTC Low", $"{crexData.Tickers[0].CrexLowPrice.DecimalToString()}")
                .AddInlineField("24h BTC High", $"{crexData.Tickers[0].CrexHighPrice.DecimalToString()}")
                .AddField("\u200b", "Graviex")
                .AddInlineField("Price", $"{graviexData.Ticker.Last.ToString()}")
                .AddInlineField("24h Volume BTC", $"{graviexData.Ticker.VolumeBtc.ToString()}")
                .AddInlineField("24h Volume IPS", $"{graviexData.Ticker.VolumeIps.ToString()}")
                .AddInlineField("24h BTC Low", $"{graviexData.Ticker.Low.ToString()}")
                .AddInlineField("24h BTC High", $"{graviexData.Ticker.High.ToString()}");

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordDataConstants.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordDataConstants.BotChannel)
                .SendMessageAsync(isBotChannel ? string.Empty : this.Context.Message.Author.Mention, false, builder.Build());
        }

        [Command("guide")]
        [Alias("guides")]
        public async Task Guide()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("Master List of Guides")
                .WithColor(Color.Blue)
                .WithDescription("\u200b")
                .WithUrl("https://github.com/ipsum-network/guides")
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")

                .AddField("PLEASE UPGRADE TO NEW WALLET VERSION ASAP", "https://github.com/ipsum-network/guides/blob/master/v3.1-UPDATE.md")
                .AddField("Full list of all guides:", "https://github.com/ipsum-network/guides")
                .AddField("Configuration Seed List:", "https://github.com/ipsum-network/seeds");

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordDataConstants.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordDataConstants.BotChannel)
                .SendMessageAsync(isBotChannel ? string.Empty : this.Context.Message.Author.Mention, false, builder.Build());
        }

        [Command("donate")]
        [Alias("donations")]
        public async Task Donation()
        {
            var builder = new EmbedBuilder();

            var dataBtc = this.GetBtcDonationAddressBalance("1592K4xS5QkXDStELPk9nHBEqZ5vLNAyrm");
            var dataIps = this.GetIpsDonationAddressBalance("iSv6vXhSbb7WH8D3dVHuWecZ7pGj4AJMmt");

            builder.WithTitle("")
                .WithColor(Color.Blue)
                .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")
                .AddField("Donations will be used for:", "Exchange Listings, Development, and Infrastructure")
                .AddField("IPS Donation Address:", "iSv6vXhSbb7WH8D3dVHuWecZ7pGj4AJMmt")
                .AddField("BTC Donation Address:", "1592K4xS5QkXDStELPk9nHBEqZ5vLNAyrm")
                .AddField("\u200b", "\u200b")
                .AddField("Current BTC donation balance:", $"{dataBtc.Result} BTC")
                .AddField("Current IPS donation balance:", $"{dataIps.Result} IPS");

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordDataConstants.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordDataConstants.BotChannel)
                .SendMessageAsync(isBotChannel ? string.Empty : this.Context.Message.Author.Mention, false, builder.Build());
        }

        [Command("build")]
        [Alias("version")]
        public async Task CurrentBuild()
        {
            var data = this.GetGithubReleaseData();

            var builder = new EmbedBuilder();

            if (data.Result != null)
            {
                builder.WithTitle($"The current build is: {data.Result.ReleaseName} - {data.Result.TagName}")
                    .WithColor(Color.Blue)
                    .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")
                    .WithDescription("\u200b")
                    .AddField("Please update your wallets and masternodes!", "https://github.com/ipsum-network/ips/releases");
            }
            else
            {
                builder.WithTitle($"Bot was not able to get the latest version, please check the link below for latest release")
                    .WithColor(Color.Blue)
                    .WithThumbnailUrl("https://masternodes.online/coin_image/IPS.png")
                    .WithDescription("\u200b")
                    .AddField("Please update your wallets and masternodes!", "https://github.com/ipsum-network/ips/releases");
            }

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordDataConstants.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordDataConstants.BotChannel)
                .SendMessageAsync(isBotChannel ? string.Empty : this.Context.Message.Author.Mention, false, builder.Build());
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

        private async Task<Graviex> GetGraviexData()
        {
            var response = await client.GetStringAsync($"https://graviex.net:443//api/v2/tickers/ipsbtc.json");
            var result = JsonConvert.DeserializeObject<Graviex>(response.ToString());

            return result;
        }

        private async Task<Crex> GetCrexData()
        {
            var response = await client.GetStringAsync($"https://api.crex24.com/CryptoExchangeService/BotPublic/ReturnTicker?request=[NamePairs=BTC_IPS].json");
            var crexresult = JsonConvert.DeserializeObject<Crex>(response);

            return crexresult;
        }

        private async Task<GithubRelease> GetGithubReleaseData()
        {
            client.DefaultRequestHeaders.Add("User-Agent", "request");

            var response = await client.GetStringAsync($"https://api.github.com/repos/ipsum-network/ips/releases/latest");
            var result = JsonConvert.DeserializeObject<GithubRelease>(response.ToString());

            return result;
        }

        private async Task<decimal> GetBtcDonationAddressBalance(string address)
        {
            var response = await client.GetStringAsync($"https://blockchain.info/q/addressbalance/{address}");

            return decimal.Parse(response, System.Globalization.NumberStyles.AllowDecimalPoint) / 100000000;
        }

        private async Task<decimal> GetIpsDonationAddressBalance(string address)
        {
            var response = await client.GetStringAsync($"https://explorer.ipsum.network/ext/getbalance/{address}");

            return decimal.Parse(response, System.Globalization.NumberStyles.AllowDecimalPoint);
        }
    }
}
