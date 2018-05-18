using Newtonsoft.Json;

namespace DiscordSupportBot.Models.BaseModels
{
    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
