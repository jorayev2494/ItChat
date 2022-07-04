using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Models
{
    public class Message
    {

        public int Id { get; set; }

        public string Text { get; set; }

        public string? Media { get; set; }

        public bool IsRead { get; set; }

        public string CreatedAt { get; set; }

        public LayoutOptions Position { get; set; }

    }
}
