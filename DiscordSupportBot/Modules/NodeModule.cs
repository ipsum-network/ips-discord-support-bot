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
        private JsonRpc JsonRpcClient => new JsonRpc("url", new System.Net.NetworkCredential { UserName = "username", Password = "password"});

        [Command("info")]
        public async Task GetInfo()
        {
            var result = JsonRpcClient.InvokeMethod("getinfo");

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotChannel)
                .SendMessageAsync($"{this.Context.Message.Author.Mention} {JsonConvert.DeserializeObject<dynamic>(result).ToString()}");
        }

        [Command("masternode")][Alias("mnstatus")]
        public async Task MnStatus(string pubKey)
        {
            var result = this.GetMasternodeStatus(pubKey);

            var resultString = result == null
                ? "Sorry, that Masternode adress was not found in the masternode list!"
                : $"```Rank: {result.Rank}\nStatus: {result.Status}\nAddress: {result.Address}\nVersion: {result.Version}\nLast Seen: {result.LastSeen}\nLast Paid: {result.LastPaid}```";

            await this.Context.Guild.GetTextChannel(DiscordSupportBot.Common.DiscordData.BotChannel)
                .SendMessageAsync($"{this.Context.Message.Author.Mention} {resultString}");
        }

        private Masternode GetMasternodeStatus(string pubKey)
        {
            var masternodes = this.JsonRpcClient.InvokeMethod("masternode", "list");
            var masternodesParsed = JsonConvert.DeserializeObject<MasternodesListResponse>(masternodes);

            var node = masternodesParsed.Masternodes.FirstOrDefault(mn => mn.Address.Equals(pubKey));

            return node;
        }
    }
}
