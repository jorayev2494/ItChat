using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.ViewModels.Profile
{
    public class ProfileShowViewModel : BaseViewModel
    {
        public bool ProfileLoaded
        {
            get => !IsBusy;
        }

        private Models.Profile profile;

        public Models.Profile Profile
        {
            get { return profile; }
            set {
                base.SetProperty<Models.Profile>(ref profile, value);
            }
        }

        public ProfileShowViewModel()
        {

        }

        public async Task LoadProfileAsync()
        {
            IsBusy = true;
            string profileStr = await SecureStorage.GetAsync("profile");

            if (profileStr != null)
            {
                Profile = JsonConvert.DeserializeObject<Models.Profile>(profileStr);
                IsBusy = false;
            }
        }

    }
}
