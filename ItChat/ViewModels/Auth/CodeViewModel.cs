using ItChat.Services.Http;
using ItChat.ViewModels.Auth.ServerData;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Auth
{
    [QueryProperty(nameof(PhoneCountryId), "phone_country_id")]
    [QueryProperty(nameof(Phone), "phone")]
    public class CodeViewModel : BaseViewModel
    {
        public object phoneCountryId = string.Empty;
        public object phone = string.Empty;

        public object PhoneCountryId
        {
            get => phoneCountryId;
            set => base.SetProperty(ref phoneCountryId, value);
        }

        public object Phone
        {
            get => phone;
            set => base.SetProperty(ref phone, value);
        }

        private string phoneCode = string.Empty;

        public string PhoneCode
        {
            get => phoneCode;
            set {
                if (phoneCode == string.Empty || phoneCode.Length < 6)
                {
                    base.SetProperty(ref phoneCode, value);
                }
                else
                {
                    base.SetProperty(ref phoneCode, string.Empty);
                }

                base.OnPropertyChanged(nameof(IsFullCode));
                ((Command)ContinueCommand).ChangeCanExecute();
            }
        }

        public bool IsFullCode
        {
            get => phoneCode.Length < 6;
        }

        public ICommand ResendCodeCommand { get; private set; }
        public ICommand ContinueCommand { get; private set; }

        public CodeViewModel()
        {
            ResendCodeCommand = new Command(async () => await ResendCode());
            ContinueCommand = new Command(async () => await Continue(), () => !IsFullCode);
        }

        private async Task ResendCode()
        {
            await Shell.Current.DisplayAlert("Code", phoneCode.ToString(), "Ok");
            PhoneCode = string.Empty;
        }

        private async Task Continue()
        {
            object data = new
            {
                device_id = DeviceInfo.Current.Idiom.ToString(),
                phone_country_id = phoneCountryId,
                phone_code = phoneCode,
                phone = phone
            };

            AuthorizationSuccess authorizationSuccess = await Http.PutAsync<AuthorizationSuccess>("/auth/phone/code", data);

            if (authorizationSuccess != null)
            {
                await authorizationSuccess.SaveData();

                Models.Profile profile = await Http.GetAsync<Models.Profile>("/profile");
                if (profile != null)
                {
                    string profileStr = JsonConvert.SerializeObject(profile);
                    await SecureStorage.Default.SetAsync("profile", profileStr);
                }

                await Shell.Current.GoToAsync("//tabBar/chats", true);
            }
        }
    }
}
