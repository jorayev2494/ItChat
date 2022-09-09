using ItChat.Services.Http;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Profile
{
    [QueryProperty(nameof(User), "User")]
    public class ProfileShowViewModel : BaseViewModel
    {
        public bool ProfileLoaded
        {
            get => !IsBusy;
        }

        private bool isMyProfile;

        public bool IsMyProfile
        {
            get { return isMyProfile; }
            set => base.SetProperty(ref isMyProfile, value);
        }

        private Models.User user;

        public Models.User User
        {
            get { return user; }
            set {
                base.SetProperty<Models.User>(ref user, value);
                //int AuthId = Convert.ToInt32(SecureStorage.Default.GetAsync("auth_id"));
                //IsMyProfile = AuthId == user.Id;
            }
        }

        public ICommand SendMessageCommand { get; private set; }
        public ICommand EditProfileCommand { get; private set; }

        public ProfileShowViewModel()
        {
            SendMessageCommand = new Command(async () => await SendMessage());
            EditProfileCommand = new Command(async () => await EditProfile());
        }

        //public async Task LoadProfileAsync()
        //{
        //    IsBusy = true;
        //    string profileStr = await SecureStorage.GetAsync("profile");

        //    if (profileStr != null)
        //    {
        //        User = JsonConvert.DeserializeObject<Models.User>(profileStr);
        //        IsBusy = false;
        //    }
        //}

        private async Task SendMessage()
        {
            Models.Chat chat = await Http.GetAsync<Models.Chat>($"/users/{user.Id}/chats");

            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Chat", chat }
            };

            await Shell.Current.GoToAsync("/chat", true, parameters);
        }

        private async Task EditProfile()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "User", User }
            };

            await Shell.Current.GoToAsync("/profile/edit", true, parameters);
        }

    }
}
