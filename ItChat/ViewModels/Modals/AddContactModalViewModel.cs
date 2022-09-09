using ItChat.Models;
using ItChat.Services.Http;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Modals
{
    public class AddContactModalViewModel : BaseViewModel
    {

        private Country selectedCountry;

        public Country SelcectedCountry
        {
            get { return selectedCountry; }
            set { 
                base.SetProperty(ref selectedCountry, value);
                base.SetProperty<int>(ref poneCountryId, selectedCountry.Id);
            }
        }

        private int poneCountryId;

        public int PoneCountryId
        {
            get { return poneCountryId; }
            set { base.SetProperty<int>(ref poneCountryId, value); }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { base.SetProperty<string>(ref phone, value); }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { base.SetProperty<string>(ref firstName, value); }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { base.SetProperty<string>(ref lastName, value); }
        }

        private IList<Country> countries;

        public IList<Country> Countries
        {
            get => this.countries;
            set => base.SetProperty(ref countries, value);
        }

        public ICommand CreateCommand { get; private set; }

        public AddContactModalViewModel()
        {
            CreateCommand = new Command(async () => await CreateAsync());
        }

        public async Task LoadCountriesAsync()
        {
            this.Countries = await Http.GetAsync<List<Country>>("/countries");
        }

        private async Task CreateAsync()
        {
            object contactData = new
            {
                phone_country_id = poneCountryId,
                phone = phone,
                first_name = firstName,
                last_name = lastName,
            };

            Models.User createdContact = await Http.PostAsync<Models.User>("/contacts", contactData);

            if (createdContact is Models.User)
            {
                await Shell.Current.Navigation.PopModalAsync(true);
            }
        }
    }
}
