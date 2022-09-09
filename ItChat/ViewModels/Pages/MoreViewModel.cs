using ItChat.Models;
using ItChat.Services.Http;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Pages
{
    public class MoreViewModel : BaseViewModel
    {
        private Models.User profile;

        public Models.User Profile
        {
            get { return profile; }
            set
            {
                base.SetProperty(ref profile, value);   
            }
        }

        public ICommand GoToProfileCommand { get; private set; }

        public MoreViewModel()
        {
            //profile = new Models.Profile()
            //{
            //    FirstName = "Almayra",
            //    LastName = "Zamzamy",
            //    Avatar = "https://ae01.alicdn.com/kf/H9c162bf534444e93b63d27620e9c80bft.jpg?width=800&height=800&hash=1600",
            //    Phone = "+62 1309 - 1710 - 1920",
            //};

            GoToProfileCommand = new Command(async () => await GoToProfile());
        }

        private async Task GoToProfile()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "User", Profile },
            };

            await Shell.Current.GoToAsync("/profile", parameters);
        }

        public async Task LoadProfile()
        {
            Profile = await Http.GetAsync<Models.User>("/profile");
        }

    }
}
