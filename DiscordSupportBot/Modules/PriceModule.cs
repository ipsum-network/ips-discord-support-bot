namespace DiscordSupportBot.Modules
{
    using Discord;
    using Discord.Commands;
    using DiscordSupportBot.Common;
    using DiscordSupportBot.Models.CoinMarketCap;
    using DiscordSupportBot.Models.Exchanges;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class PriceModule : ModuleBase<SocketCommandContext>
    {
        private static Listings Listings { get; set; }
        private static HttpClient client = new HttpClient();

        [Command("price")]
        [Alias("checkprice")]
        public async Task Price(string ticker)
        {
            Listings = (Listings == null || DateTime.Now.AddHours(-24) > Listings.LastUpdatedFromApi)
                ? this.GetListings().Result
                : Listings;

            var coin = Listings.Currencies.FirstOrDefault(c => c.Symbol.Equals(ticker.ToUpperInvariant()));

            Currency prices = null;

            if (coin != null)
            {
                prices = await this.GetPrices(coin.Id);
            }

            var resultString = prices != null
                ? prices.CoinData.Id != 1
                    ? $"```Currency: {prices.CoinData.Name}\nTicker: {prices.CoinData.Symbol}\nPrice USD: {prices.CoinData.Quotes.PriceUSD.Price}\nPrice BTC: {prices.CoinData.Quotes.PriceBTC.Price}\nChange(24h): {prices.CoinData.Quotes.PriceUSD.PercentChange24h}%```"
                    : $"```Currency: {prices.CoinData.Name}\nTicker: {prices.CoinData.Symbol}\nPrice USD: {prices.CoinData.Quotes.PriceUSD.Price}\nChange(24h): {prices.CoinData.Quotes.PriceUSD.PercentChange24h}%```"
                : $"Could not get the price.";

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordSupportBot.Common.DiscordData.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotChannel)
                .SendMessageAsync($"{(isBotChannel ? resultString : $"{this.Context.Message.Author.Mention} {resultString}")}");
        }

        public async Task<Listings> GetListings()
        {
            var response = await client.GetStringAsync($"https://api.coinmarketcap.com/v2/listings/");
            var result = JsonConvert.DeserializeObject<Listings>(response.ToString());

            return result;
        }

        public async Task<Currency> GetPrices(int id)
        {
            var response = await client.GetStringAsync($"https://api.coinmarketcap.com/v2/ticker/{id}/?convert=BTC");
            var result = JsonConvert.DeserializeObject<Currency>(response.ToString());

            return result;
        }
    }
}
