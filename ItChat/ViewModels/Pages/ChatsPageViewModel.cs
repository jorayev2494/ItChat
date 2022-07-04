using ItChat.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Pages
{
    public class ChatsPageViewModel : BaseViewModel
    {

        private IReadOnlyCollection<Story> stories;
        private IReadOnlyCollection<ItChat.Models.Chat> chats;

        public IReadOnlyCollection<Story> Stories
        {
            get => stories;
            set => SetProperty(ref stories, value);
        }

        public IReadOnlyCollection<ItChat.Models.Chat> Chats
        {
            get => chats;
            set => SetProperty(ref chats, value);
        }

        public ICommand SellectedStoryCommand { get; private set; }

        public ICommand SellectedChatCommand { get; private set; }

        public ICommand AddStoryCommand { get; private set; }

        public ChatsPageViewModel()
        {
            Stories = new List<Story>()
            {
                new Story() { Id = 1, FullName = "First-name Last-name 1", Media = "https://shutnikov.club/wp-content/uploads/2020/04/The_cat_with_glasses_on_the_avatar_in_the_social_network_1_11093937.jpg" },
                new Story() { Id = 2, FullName = "First-name Last-name 2", Media = "https://shutnikov.club/wp-content/uploads/2020/04/kot_v_ochkah_5_11093943.jpg" },
                new Story() { Id = 3, FullName = "First-name Last-name 3", Media = "https://store.playstation.com/store/api/chihiro/00_09_000/container/TR/tr/99/EP2402-CUSA05624_00-AV00000000000193/0/image?_version=00_09_000&platform=chihiro&bg_color=000000&opacity=100&w=720&h=720" },
                new Story() { Id = 4, FullName = "First-name Last-name 4", Media = "https://upload.wikimedia.org/wikipedia/commons/7/77/Avatar_cat.png" },
                new Story() { Id = 5, FullName = "First-name Last-name 5", Media = "https://avatarfiles.alphacoders.com/239/239189.jpg" },
                new Story() { Id = 6, FullName = "First-name Last-name 6", Media = "https://static.wikia.nocookie.net/5afcf21d-3ed9-45db-a15f-263f0e617ed6" },
                new Story() { Id = 7, FullName = "First-name Last-name 7", Media = "https://avatarfiles.alphacoders.com/154/154947.jpg" },
                new Story() { Id = 8, FullName = "First-name Last-name 8", Media = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRotj3N6mrXVapwYH2H_0hYQNPW_JGlexy8cj709iS_le5_YP5K3TZcoSudtoODnPK65mU&usqp=CAU" },
                new Story() { Id = 9, FullName = "First-name Last-name 9", Media = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRVOW7nLPJmMWvyYW6MKwMbJtzvmoQC7sPJYZH9aPX1F72wBEhNUXu9Giw1KU4OG8UhTKM&usqp=CAU" },
                new Story() { Id = 10, FullName = "First-name Last-name 10", Media = "https://exploringbits.com/wp-content/uploads/2022/01/cat-pfp-1.jpg?ezimgfmt=rs:352x384/rscb3/ng:webp/ngcb3" },
                new Story() { Id = 11, FullName = "First-name Last-name 11", Media = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRJgUCAZ2LhR9mPO-STAKF_4Kr3kuuq_wjXiUX-NnVugjedfRsN9xVSi9jpDb8NPPhTWBY&usqp=CAU" },
                new Story() { Id = 12, FullName = "First-name Last-name 12", Media = "https://i.pinimg.com/474x/93/27/e7/9327e7da553a3111959de04fdf2e2eb4.jpg" },
            };

            Chats = new ObservableCollection<ItChat.Models.Chat>()
            {
                new ItChat.Models.Chat() {
                    Id = 1,
                    FullName = "Chat-name Last-name 1",
                    Avatar = "https://i.pinimg.com/474x/93/27/e7/9327e7da553a3111959de04fdf2e2eb4.jpg",
                    LastMessage = "Good morning",
                    CountDontViewed = 1,
                    IsActive = true,
                    CreatedAt = "Today"
                },
                new ItChat.Models.Chat() {
                    Id = 2,
                    FullName = "Chat-name Last-name 2",
                    Avatar = "https://exploringbits.com/wp-content/uploads/2022/01/cat-pfp-1.jpg?ezimgfmt=rs:352x384/rscb3/ng:webp/ngcb3",
                    LastMessage = "Hello world!",
                    CountDontViewed = null,
                    IsActive = false,
                    CreatedAt = "11:38"
                },
                new ItChat.Models.Chat() {
                    Id = 3,
                    FullName = "Chat-name Last-name 3",
                    Avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRJgUCAZ2LhR9mPO-STAKF_4Kr3kuuq_wjXiUX-NnVugjedfRsN9xVSi9jpDb8NPPhTWBY&usqp=CAU",
                    LastMessage = "How is it going?",
                    CountDontViewed = 1,
                    IsActive = false,
                    CreatedAt = "09:23"
                },
                new ItChat.Models.Chat() {
                    Id = 4,
                    FullName = "Chat-name Last-name 4",
                    Avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRotj3N6mrXVapwYH2H_0hYQNPW_JGlexy8cj709iS_le5_YP5K3TZcoSudtoODnPK65mU&usqp=CAU",
                    LastMessage = "How is it going?",
                    CountDontViewed = 1,
                    IsActive = false,
                    CreatedAt = "Yesterday"
                },
                new ItChat.Models.Chat() {
                    Id = 5,
                    FullName = "Chat-name Last-name 5",
                    Avatar = "https://avatarfiles.alphacoders.com/154/154947.jpg",
                    LastMessage = "awd a daw daad awd aw daw awd awd ad awd awdawd aw awd awd wa",
                    CountDontViewed = null,
                    IsActive = false,
                    CreatedAt = "23:12"
                },
                new ItChat.Models.Chat() {
                    Id = 6,
                    FullName = "Chat-name Last-name 6",
                    Avatar = "https://static.wikia.nocookie.net/5afcf21d-3ed9-45db-a15f-263f0e617ed6",
                    LastMessage = "awd aw dadawd awd a",
                    CountDontViewed = null,
                    IsActive = false,
                    CreatedAt = "04:13"
                },
                new ItChat.Models.Chat() {
                    Id = 7,
                    FullName = "Chat-name Last-name 7",
                    Avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRJgUCAZ2LhR9mPO-STAKF_4Kr3kuuq_wjXiUX-NnVugjedfRsN9xVSi9jpDb8NPPhTWBY&usqp=CAU",
                    LastMessage = "aw dawd awda da",
                    CountDontViewed = null,
                    IsActive = false,
                    CreatedAt = "01:01"
                },
                new ItChat.Models.Chat() {
                    Id = 8,
                    FullName = "Chat-name Last-name 8",
                    Avatar = "https://store.playstation.com/store/api/chihiro/00_09_000/container/TR/tr/99/EP2402-CUSA05624_00-AV00000000000193/0/image?_version=00_09_000&platform=chihiro&bg_color=000000&opacity=100&w=720&h=720",
                    LastMessage = "Howawd ad awawd awd awda aad",
                    CountDontViewed = 1,
                    IsActive = false,
                    CreatedAt = "on Modnday"
                },
                new ItChat.Models.Chat() {
                    Id = 9,
                    FullName = "Chat-name Last-name 9",
                    Avatar = "https://i.pinimg.com/474x/93/27/e7/9327e7da553a3111959de04fdf2e2eb4.jpg",
                    LastMessage = "awd aw dwad aw daw dawd",
                    CountDontViewed = 1,
                    IsActive = true,
                    CreatedAt = "21:25"
                },
                new ItChat.Models.Chat() {
                    Id = 10,
                    FullName = "Chat-name Last-name 10",
                    Avatar = "https://exploringbits.com/wp-content/uploads/2022/01/cat-pfp-1.jpg?ezimgfmt=rs:352x384/rscb3/ng:webp/ngcb3",
                    LastMessage = "awd wadawdad aw dawd",
                    CountDontViewed = null,
                    IsActive = false,
                    CreatedAt = "13:57"
                },
            };

            SellectedStoryCommand = new Command<Story>(async (Story story) => await SelectedStory(story));
            SellectedChatCommand = new Command<ItChat.Models.Chat>(async (ItChat.Models.Chat chat) => await SelctedChat(chat));
            AddStoryCommand = new Command(async () => await AddStory());
        }

        private async Task SelectedStory(Story story)
        {
            await Shell.Current.DisplayAlert("Selected story", story.FullName, "Ok");
        }

        private async Task SelctedChat(ItChat.Models.Chat chat)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Chat", chat }
            };

            await Shell.Current.GoToAsync("/chat", parameters);
        }

        private async Task AddStory()
        {
            await Shell.Current.DisplayAlert("Add story", "Add story", "Ok");
        }

    }
}
