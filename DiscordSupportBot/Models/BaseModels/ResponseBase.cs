namespace DiscordSupportBot.Models.BaseModels
{
    public class ResponseBase
    {
        public bool Success => this.Error == null;

        public Error Error { get; set; }

        public string Id { get; set; }
    }
}
