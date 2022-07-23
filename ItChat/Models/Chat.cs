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

        [JsonProperty("status_id")]
        public string StatusId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("members"), JsonIgnore]
        public IList<User> Members { get; set; }

        [JsonProperty("messages")]
        public IList<Message> Messages { get; set; }

        //public int? CountDontViewed { get; set; }

        //public bool IsActive { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

    }
}
