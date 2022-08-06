using ItChat.Models;
using ItChat.Services.Http;
using ItChat.Services.Pusher;
using MvvmHelpers;
using Newtonsoft.Json;
using PusherClient;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Windows.Input;
using ItChat.Services.Pusher.Resources.Chats;

namespace ItChat.ViewModels.Pages
{
    public class ChatsPageViewModel : BaseViewModel
    {

        private IReadOnlyCollection<Story> stories;
        private ObservableCollection<ItChat.Models.Chat> chats;
        private Models.Chat selectedChat;

        public IReadOnlyCollection<Story> Stories
        {
            get => stories;
            set => SetProperty(ref stories, value);
        }

        public ObservableCollection<ItChat.Models.Chat> Chats
        {
            get => chats;
            set => SetProperty(ref chats, value);
        }

        public ICommand SellectedStoryCommand { get; private set; }

        public ICommand SellectedChatCommand { get; private set; }

        public ICommand AddStoryCommand { get; private set; }

        public ICommand LogoutCommand { get; private set; }

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

            SellectedStoryCommand = new Command<Story>(async (Story story) => await SelectedStory(story));
            SellectedChatCommand = new Command<ItChat.Models.Chat>(async (ItChat.Models.Chat chat) => await SelctedChat(chat));
            AddStoryCommand = new Command(async () => await AddStory());
            LogoutCommand = new Command(async () => await Logout());
        }

        public async Task LoadChataAsync()
        {
            string accessToken = await SecureStorage.Default.GetAsync("access_token");

            if (accessToken != null)
            {
                this.Chats = await Http.GetAsync<ObservableCollection<ItChat.Models.Chat>>("/chats");
            }
        }

        private async Task SelectedStory(Story story)
        {
            await Shell.Current.DisplayAlert("Selected story", story.FullName, "Ok");
        }

        private async Task SelctedChat(ItChat.Models.Chat chat)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Chat", selectedChat = chat }
            };

            await Shell.Current.GoToAsync("/chat", parameters);
        }

        private async Task AddStory()
        {
            await Shell.Current.DisplayAlert("Add story", "Add story", "Ok");
        }

        public async Task PusherChatConnection()
        {
            string accessToken = await SecureStorage.Default.GetAsync("access_token");

            if (accessToken == null)
            {
                return;
            }

            HttpAuthorizer httpAuthorizer = new CustomPusherAuthorizer("http://185.81.167.88:8000/websockets/auth")
            {
                AuthenticationHeader = new AuthenticationHeaderValue("Authorization", $"Bearer {accessToken}"),
            };

            Pusher pusher = new Pusher("laravel_rdb", new PusherOptions()
            {
                Authorizer = httpAuthorizer,
                // Cluster = "mt1",
                Encrypted = !true,
                Host = "185.81.167.88:6001"
            });

            pusher.Error += ErrorHandler;

            await pusher.ConnectAsync().ConfigureAwait(false);
            Channel channel = await pusher.SubscribeAsync($"private-chat.1", new SubscriptionEventHandler((object sender) => {
                Console.WriteLine("--- SubscriptionEventHandler: {0}", sender.ToString());
            }));

            channel.Bind("message.sent", (PusherEvent pusherEvent) => {
                Console.WriteLine("--- PusherEvent user_id: {0}, PusherEvent data: {1}", pusherEvent.UserId, pusherEvent.Data);

                ChatPusherResource chatPusherResource = JsonConvert.DeserializeObject<ChatPusherResource>(pusherEvent.Data);

                //Models.Chat foundChat = Chats.First<Models.Chat>((Models.Chat ch) => ch.Id == chatPusherResource.Chat.Id);
                Models.Chat foundChat = Chats.First<Models.Chat>((Models.Chat ch) => ch.Id == chatPusherResource.Chat.Id);

                if (foundChat is Models.Chat)
                {
                    Console.WriteLine("--- FoundChat.Id: {0}, LastMessage.Text: {1}", foundChat.Id, foundChat.LastMessage.Text);
                    //int foundChatIndex = Chats.IndexOf(foundChat);
                    //Chats[foundChatIndex].LastMessage = chatPusherResource.Chat.LastMessage;
                    Chats.Remove(foundChat);
                    Chats.Insert(0, chatPusherResource.Chat);

                }

                //if (selectedChat.Id == chatPusherResource.Chat.Id)
                //{
                //    MessagingCenter.Send<BaseViewModel, Message>(this, $"chat.{chatPusherResource.Chat.Id}", chatPusherResource.Message);
                //}
                //Messages.Add(ChatPusherResource.Message);
            });
        }

        private async Task Logout()
        {
            SecureStorage.Default.RemoveAll();
            await Shell.Current.GoToAsync("/login", true);
        }

        void ErrorHandler(object sender, PusherException error)
        {
            if ((int)error.PusherCode < 5000)
            {
                // Error recevied from Pusher cluster, use PusherCode to filter.
            }
            else
            {
                if (error is ChannelUnauthorizedException unauthorizedAccess)
                {
                    // Private and Presence channel failed authorization with Forbidden (403)
                }
                else if (error is ChannelAuthorizationFailureException httpError)
                {
                    // Authorization endpoint returned an HTTP error other than Forbidden (403)
                }
                else if (error is OperationTimeoutException timeoutError)
                {
                    // A client operation has timed-out. Governed by PusherOptions.ClientTimeout
                }
                else if (error is ChannelDecryptionException decryptionError)
                {
                    // Failed to decrypt the data for a private encrypted channel
                }
                else
                {
                    // Handle other errors
                }
            }
        }

    }
}
