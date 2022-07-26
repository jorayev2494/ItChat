using MvvmHelpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ItChat.Models;
using System.Collections.ObjectModel;

namespace ItChat.ViewModels.Pages
{
    public class ContactsViewModel : BaseViewModel
    {

        private ObservableCollection<Models.Contact> contacts;

        public ObservableCollection<Models.Contact> Contacts
        {
            get => contacts;
            set
            {
                base.SetProperty(ref contacts, value);
            }
        }

        public ICommand SelectedContactCommand;

        public ContactsViewModel()
        {
            loadContacts();
            SelectedContactCommand = new Command<Models.Contact>(async (Models.Contact c) => await SelectedConatactAsync(c));
        }

        private void loadContacts()
        {
            Contacts = new ObservableCollection<Models.Contact>()
            {
                new Models.Contact() { Id = 1, FirstName = "Athalia", LastName = "Putri", Avatar = "https://shutnikov.club/wp-content/uploads/2020/04/The_cat_with_glasses_on_the_avatar_in_the_social_network_1_11093937.jpg", IsOnline = true },
                new Models.Contact() { Id = 2, FirstName = "Erlan", LastName = "Sadewa", Avatar = "https://shutnikov.club/wp-content/uploads/2020/04/kot_v_ochkah_5_11093943.jpg", IsOnline = false },
                new Models.Contact() { Id = 3, FirstName = "Midala", LastName = "Huera", Avatar = "https://store.playstation.com/store/api/chihiro/00_09_000/container/TR/tr/99/EP2402-CUSA05624_00-AV00000000000193/0/image?_version=00_09_000&platform=chihiro&bg_color=000000&opacity=100&w=720&h=720", IsOnline = true },
                new Models.Contact() { Id = 4, FirstName = "Nafisa", LastName = "Gitari", Avatar = "https://upload.wikimedia.org/wikipedia/commons/7/77/Avatar_cat.png", IsOnline = true },
                new Models.Contact() { Id = 5, FirstName = "Raki", LastName = "Devon", Avatar = "https://avatarfiles.alphacoders.com/239/239189.jpg", IsOnline = true },
                new Models.Contact() { Id = 6, FirstName = "Salsabila", LastName = "Akira", Avatar = "https://static.wikia.nocookie.net/5afcf21d-3ed9-45db-a15f-263f0e617ed6", IsOnline = false },
                new Models.Contact() { Id = 7, FirstName = "Aygul", LastName = "Nazarova", Avatar = "https://avatarfiles.alphacoders.com/154/154947.jpg", IsOnline = true },
                new Models.Contact() { Id = 8, FirstName = "Yakub", LastName = "Jorayev", Avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRotj3N6mrXVapwYH2H_0hYQNPW_JGlexy8cj709iS_le5_YP5K3TZcoSudtoODnPK65mU&usqp=CAU", IsOnline = true },
                new Models.Contact() { Id = 9, FirstName = "Jemshit", LastName = "Jorayev", Avatar = "https://exploringbits.com/wp-content/uploads/2022/01/cat-pfp-1.jpg?ezimgfmt=rs:352x384/rscb3/ng:webp/ngcb3", IsOnline = true },
                new Models.Contact() { Id = 10, FirstName = "Merjen", LastName = "Jorayeva", Avatar = "https://i.pinimg.com/474x/93/27/e7/9327e7da553a3111959de04fdf2e2eb4.jpg", IsOnline = true },
                new Models.Contact() { Id = 11, FirstName = "Athalia", LastName = "Putri", Avatar = "https://shutnikov.club/wp-content/uploads/2020/04/The_cat_with_glasses_on_the_avatar_in_the_social_network_1_11093937.jpg", IsOnline = true },
                new Models.Contact() { Id = 12, FirstName = "Erlan", LastName = "Sadewa", Avatar = "https://shutnikov.club/wp-content/uploads/2020/04/kot_v_ochkah_5_11093943.jpg", IsOnline = false },
                new Models.Contact() { Id = 13, FirstName = "Midala", LastName = "Huera", Avatar = "https://store.playstation.com/store/api/chihiro/00_09_000/container/TR/tr/99/EP2402-CUSA05624_00-AV00000000000193/0/image?_version=00_09_000&platform=chihiro&bg_color=000000&opacity=100&w=720&h=720", IsOnline = true },
                new Models.Contact() { Id = 14, FirstName = "Nafisa", LastName = "Gitari", Avatar = "https://upload.wikimedia.org/wikipedia/commons/7/77/Avatar_cat.png", IsOnline = true },
                new Models.Contact() { Id = 15, FirstName = "Raki", LastName = "Devon", Avatar = "https://avatarfiles.alphacoders.com/239/239189.jpg", IsOnline = true },
            };
        }

        private async Task SelectedConatactAsync(Models.Contact contact)
        {
            await Shell.Current.DisplayAlert("Selected contact", contact.FullName, "Ok");
        }

    }
}
