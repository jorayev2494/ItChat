using ItChat.Models.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Models
{
    public class Message : IEntity
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("chat_id")]
        public uint ChatId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("parent_id")]
        public uint? ParentId { get; set; }

        [JsonProperty("is_seen")]
        public bool IsSeen { get; set; }

        [JsonProperty("medias")]
        public ObservableCollection<Media> Medias { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonIgnore]
        public LayoutOptions Position { get; set; }

    }
}
