using ItChat.Models.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Models
{
    public class Chat : IEntity
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("avatar")]
        public string? Avatar { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("messages_unseen_count")]
        public uint MessagesUnseenCount { get; set; }

        [JsonProperty("last_message")]
        public Message LastMessage { get; set; }

        [JsonProperty("members"), JsonIgnore]
        public IList<User> Members { get; set; }

        //public int? CountDontViewed { get; set; }

        //public bool IsActive { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

    }
}
