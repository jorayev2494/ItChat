using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Models.Contracts
{
    public interface IEntity
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
    }
}
