namespace DiscordSupportBot.Modules
{
    using Common;
    using Discord;
    using Discord.Commands;
    using DiscordSupportBot.Models.BaseModels;
    using DiscordSupportBot.Models.Masternode;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NodeModule : ModuleBase<SocketCommandContext>
    {
        private JsonRpc JsonRpcClient => new JsonRpc("url", new System.Net.NetworkCredential { UserName = "username", Password = "password" });

        [Command("info")]
        public async Task Info()
        {
            var result = JsonRpcClient.InvokeMethod("getinfo");

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotChannel)
                .SendMessageAsync($"{JsonConvert.DeserializeObject<dynamic>(result).ToString()}");
        }

        [Command("masternode")]
        [Alias("mnstatus")]
        public async Task MnStatus(string pubKey)
        {
            var result = this.GetMasternodeStatus(pubKey);

            var resultString = result == null
                ? "Sorry, that Masternode adress was not found in the masternode list!"
                : $"```Rank: {result.Rank}\nStatus: {result.Status}\nAddress: {result.Address}\nVersion: {result.Version}\nLast Seen: {result.LastSeen.ParseEpochToDateTime()}\nLast Paid: {result.LastPaid.ParseEpochToDateTimeLastPaid()}```";

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordSupportBot.Common.DiscordData.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotChannel)
                .SendMessageAsync($"{(isBotChannel ? resultString : $"{this.Context.Message.Author.Mention} {resultString}")}");
        }

        [Command("mnconnect")]
        [Alias("mnconstatus")]
        public async Task MnConnectionStatus(string ipPort)
        {
            var result = this.GetMasternodeConnectionStatus(ipPort);

            var resultString = result.Success
                ? $"```Connection to {ipPort} was successful!```"
                : $"```Connection to {ipPort} was unsuccessul, reason: {result.Error.Message}```";

            var isBotChannel = this.Context.Channel.Id.Equals(DiscordSupportBot.Common.DiscordData.BotChannel);

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotChannel)
                .SendMessageAsync($"{(isBotChannel ? resultString : $"{this.Context.Message.Author.Mention} {resultString}")}");
        }

        private Masternode GetMasternodeStatus(string pubKey)
        {
            var masternodes = this.JsonRpcClient.InvokeMethod("masternode", "list");
            var masternodesParsed = JsonConvert.DeserializeObject<MasternodesListResponse>(masternodes);

            var node = masternodesParsed.Masternodes.FirstOrDefault(mn => mn.Address.Equals(pubKey));

            return node;
        }

        private ResponseBase GetMasternodeConnectionStatus(string ipPort)
        {
            var statusResponse = this.JsonRpcClient.InvokeMethod("masternode", new[] { "connect", ipPort });
            var status = JsonConvert.DeserializeObject<ResponseBase>(statusResponse);

            return status;
        }
    }
}
