using ItChat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Services.Pusher.Resources.Chats
{
    public class ChatPusherResource
    {

        [JsonProperty("chat")]
        public Chat Chat { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

    }
}
