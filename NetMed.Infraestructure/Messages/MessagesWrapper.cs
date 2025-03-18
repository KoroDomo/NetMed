using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMed.Infraestructure.Messages
{
    public class MessagesWrapper
    {
        [JsonProperty("messagesAppointments")]
        public List<MessageItem> MessagesAppointments { get; set; }
    }
}
