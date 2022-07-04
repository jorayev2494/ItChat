using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Models
{
    public class Profile
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{this.FullName} {this.LastName}"; }
        public string Avatar { get; set; }
        public string Phone { get; set; }

    }
}
