using Newtonsoft.Json;


namespace WebApiApplication.Infraestructura.Messages
{
    public class MessageItem
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
