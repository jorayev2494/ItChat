using ItChat.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Modals
{
    public class SelectCountryModalViewModel : BaseViewModel
    {

        private IList<Country> countries;

        public IList<Country> Countries
        {
            get => countries;
            set
            {
                base.SetProperty(ref countries, value);
            }
        }

        private Country country;

        public Country Country
        {
            get => country;
            set {
                base.SetProperty(ref country, value);
            }
        }

        public ICommand SelectCountryCommand { get; private set; }

        public SelectCountryModalViewModel(IList<Country> countries)
        {
            Countries = countries;
            SelectCountryCommand = new Command<Country>(async (Country ctr) => await SelectCountry(ctr));
        }

        private async Task SelectCountry(Country country)
        {
            await Shell.Current.Navigation.PopAsync();
            await Shell.Current.DisplayAlert("Selected country", country.Name, "Ok");
        }
    }
}
