using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Models
{
    public sealed class Country
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("phone_code")]
        public string PhoneCode { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("flag_svg")]
        public string FlagSVG { get; set; }

        public string PikerItemDisplay => string.Concat(Flag, "  ", Name);
        public string PikerTitle => string.Concat(Flag, "  ", PhoneCode);

    }
}
