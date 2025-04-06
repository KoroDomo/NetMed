using Newtonsoft.Json;

namespace WebApiApplication.Infraestructura.Messages
{
    public class MessagesWrapper
    {
        [JsonProperty("messagesAppointments")]
        public List<MessageItem> MessagesAppointments { get; set; }
    }
}
