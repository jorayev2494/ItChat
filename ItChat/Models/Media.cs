using ItChat.Models.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Models
{
    public class Media : IEntity
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("with")]
        public uint? Width { get; set; }

        [JsonProperty("height")]
        public uint? Height { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("user_file_name")]
        public string UserFileName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
