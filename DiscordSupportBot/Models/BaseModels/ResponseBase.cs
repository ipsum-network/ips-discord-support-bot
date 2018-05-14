namespace DiscordSupportBot.Models.BaseModels
{
    public class ResponseBase
    {
        public bool Success { get; set; }

        public string Error { get; set;}

        public string Message { get; set; }

        public string Id { get; set; }
    }
}
