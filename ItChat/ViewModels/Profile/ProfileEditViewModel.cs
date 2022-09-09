using ItChat.Services.Http;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Profile
{
    [QueryProperty(nameof(User), "User")]
    public class ProfileEditViewModel : BaseViewModel
    {

        private Models.User user;

        public Models.User User
        {
            get { return user; }
            set { base.SetProperty(ref user, value); }
        }

        private IList<Models.Country> countries;

        public IList<Models.Country> Countries
        {
            get => countries;
            set {
                SetProperty(ref countries, value);
            }
        }

        public ICommand UpdateProfileCommand { get; private set; }

        public ProfileEditViewModel()
        {
            UpdateProfileCommand = new Command(async () => await UpdateProfile());
        }

        public async Task LoadCountries()
        {
            Countries = await Http.GetAsync<List<Models.Country>>("/countries");
        }

        private async Task UpdateProfile()
        {
            Models.User updatedProfile = await Http.PostAsync<Models.User>("/profile", User);

            if (updatedProfile is Models.User)
            {
                await Shell.Current.DisplayAlert("Success updated profile", updatedProfile.FullName, "Ok");
            }
        }        

    }
}
