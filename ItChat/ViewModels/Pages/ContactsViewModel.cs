using MvvmHelpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ItChat.Models;
using System.Collections.ObjectModel;
using ItChat.Services.Http;
using ItChat.Views.Modals;

namespace ItChat.ViewModels.Pages
{
    public class ContactsViewModel : BaseViewModel
    {

        private ObservableCollection<Models.User> contacts;

        public ObservableCollection<Models.User> Contacts
        {
            get => contacts;
            set
            {
                base.SetProperty(ref contacts, value);
            }
        }

        public ICommand SelectedContactCommand { get; private set; }
        public ICommand AddContactCommand { get; private set; }

        public ContactsViewModel()
        {
            SelectedContactCommand = new Command<Models.User>(async (Models.User u) => await SelectedConatactAsync(u));
            AddContactCommand = new Command(async () => await AddContact());
        }

        public async void loadContacts()
        {
            Contacts = await Http.GetAsync<ObservableCollection<Models.User>>("/contacts");
        }

        private async Task SelectedConatactAsync(Models.User user)
        {
            //Shell.Current.DisplayAlert("SelectedConatactAsync: ", user.FullName.ToString(), "Ok");

            IDictionary<string, object> profileParams = new Dictionary<string, object>()
            {
                { "User", user }
            };

            await Shell.Current.GoToAsync("/profile", profileParams);
        }

        private async Task AddContact()
        {
            await Shell.Current.Navigation.PushModalAsync(new AddContactModal(), true);
        }

    }
}
