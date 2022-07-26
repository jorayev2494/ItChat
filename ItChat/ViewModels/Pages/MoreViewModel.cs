using ItChat.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.ViewModels.Pages
{
    public class MoreViewModel : BaseViewModel
    {
        private Models.Profile profile;

        public Models.Profile Profile
        {
            get { return profile; }
            set
            {
                base.SetProperty(ref profile, value);
            }
        }

        public MoreViewModel()
        {
            profile = new Models.Profile()
            {
                FirstName = "Almayra",
                LastName = "Zamzamy",
                Avatar = "https://ae01.alicdn.com/kf/H9c162bf534444e93b63d27620e9c80bft.jpg?width=800&height=800&hash=1600",
                Phone = "+62 1309 - 1710 - 1920",
            };
        }

    }
}
