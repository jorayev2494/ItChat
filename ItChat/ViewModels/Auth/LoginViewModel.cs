using ItChat.Models;
using ItChat.Services.Http;
using ItChat.Views.Modals;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        private Country selectedCountry;

        public Country SelcectedCountry
        {
            get { return selectedCountry; }
            set { base.SetProperty(ref selectedCountry, value); }
        }

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

        private IList<Country> countries;

        public IList<Country> Countries
        {
            get => this.countries;
            set => base.SetProperty(ref countries, value);
        }

        public ICommand ContinueCommand { get; private set; }

        public ICommand OpenSelectCountryModalCommand { get; private set; }

        public LoginViewModel()
        {
            LoadCountriesAsync();
            this.ContinueCommand = new Command(async () => await this.Continue(), () => this.PhoneNumber != string.Empty);
            OpenSelectCountryModalCommand = new Command(async () => await OpenSelectCountryModal());
        }

        public async Task LoadCountriesAsync()
        {
            this.Countries = await Http.GetAsync<List<Country>>("/countries");
        }

        private async Task Continue()
        {
            object data = new {
                phone_country_id = SelcectedCountry.Id,
                phone = phoneNumber
            };

            object result = await Http.PostAsync<object>("/auth/phone/code", data);

            if (result is not null)
            {
                await Shell.Current.DisplayAlert("Success", $"Code is {result}", "Ok");
                Dictionary<string, object> parameters = new Dictionary<string, object>() {
                    { "phone_country_id", SelcectedCountry.Id },
                    { "phone", phoneNumber }
                };
                await Shell.Current.GoToAsync("/code", true, parameters);
            }
        }

        private async Task OpenSelectCountryModal()
        {
            await Shell.Current.Navigation.PushModalAsync(new SelectCountryModal(Countries));
        }

        //private async Task SelectCountry()
        //{
        //    await Shell.Current.CurrentPage.
        //}

    }
}
