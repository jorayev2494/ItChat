using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Auth
{
    public class LoginViewModel : BaseViewModel
    {

        private string countryCode;

        public string ConuntryCode
        {
            get => this.countryCode;
            set => base.SetProperty<string>(ref countryCode, value);
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get => this.phoneNumber;
            set => base.SetProperty<string>(ref phoneNumber, value);
        }

        public ICommand ContinueCommand { get; private set; }

        public LoginViewModel()
        {
            this.ContinueCommand = new Command(async () => await this.Continue(), () => this.PhoneNumber != string.Empty);
        }

        private async Task Continue()
        {
            await Shell.Current.DisplayAlert("Allert", $"Your phone is {this.PhoneNumber}", "Ok");
            this.PhoneNumber = string.Empty;
        }

    }
}
