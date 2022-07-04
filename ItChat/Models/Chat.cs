using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Models
{
    public class Chat
    {

        public int Id { get; set; }

        public string FullName { get; set; }

        public string Avatar { get; set; }

        public string LastMessage { get; set; }

        public int? CountDontViewed { get; set; }

        public bool IsActive { get; set; }

        public string CreatedAt { get; set; }

    }
}
