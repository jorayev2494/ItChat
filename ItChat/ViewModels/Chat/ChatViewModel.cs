using ItChat.Models;
using ItChat.Services.Http;
using ItChat.Services.Pusher;
using ItChat.Services.Pusher.Resources.Chats;
using MvvmHelpers;
using Newtonsoft.Json;
using PusherClient;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace ItChat.ViewModels.Chat
{
    [QueryProperty(nameof(Chat), "Chat")]
    public class ChatViewModel : BaseViewModel
    {

        private string text = string.Empty;

        public string Text
        {
            get => this.text;
            set {
                base.SetProperty(ref text, value);
                ((Command)SendMessageCommand).ChangeCanExecute();
            }
        }

        private Models.Chat chat;

        public Models.Chat Chat
        {
            get => this.chat;
            set {
                base.SetProperty(ref chat, value);
            }
        }

        private ObservableCollection<Message> messages;

        public ObservableCollection<Message> Messages
        {
            get { return messages; }
            set { base.SetProperty(ref messages, value); }
        }

        public ICommand SendMessageCommand { get; private set; }
        public ICommand PickMediaCommand { get; private set; }

        public ChatViewModel()
        {
            SendMessageCommand = new Command(SendMessage, () => text.Length > 1);
            PickMediaCommand = new Command(async () => await PickMedia());    
        }

        public async Task LoadChat()
        {
            IList<Message> serverMessages = await Http.GetAsync<List<Message>>($"/chats/{chat.Id}");
            Messages = new ObservableCollection<Message>();

            foreach (Message msg in serverMessages)
            {
                msg.Position = msg.User.Id == 13 ? LayoutOptions.End : LayoutOptions.Start;
                Messages.Add(msg);
            }

            //MessagingCenter.Subscribe<BaseViewModel, Message>(this, $"chat.{chat.Id}", (BaseViewModel sender, Message msg) =>
            //{
            //    Messages.Add(msg);
            //    //Shell.Current.DisplayAlert("New message", msg.Text, "Ok");
            //});
        }

        private async void SendMessage()
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            object dataMessage = new { chat_id = chat.Id, text = text };

            Message sentMsg = await Http.PostAsync<Message>($"/chats", dataMessage);

            Messages.Add(sentMsg);
            Text = string.Empty;
        }

        private async Task PickMedia()
        {
            try
            {
                FileResult media = await MediaPicker.PickVideoAsync();
                await LoadPhotoAsync(media);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        private async Task LoadPhotoAsync(FileResult media)
        {

            if (media == null)
            {
                return;
            }

            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            multipartFormDataContent.Add(
                new StreamContent(await media.OpenReadAsync()),
                "medias[]",
                media.FileName
            );

            StringContent httpContent = new StringContent(chat.Id.ToString());
            multipartFormDataContent.Add(httpContent, "chat_id");
            //StringContent httpContenta = new StringContent(this.Text);
            //multipartFormDataContent.Add(httpContenta, "text");

            Message sentMsg = await Http.PostAsync<Message>("/chats", multipartFormDataContent);
            Messages.Add(sentMsg);

            //string mediaPath = Path.Combine(FileSystem.CacheDirectory, media.FileName);
            //using (var stream = await media.OpenReadAsync())
            //{
            //    using (var newStream = File.OpenWrite(mediaPath))
            //    {
            //        await stream.CopyToAsync(newStream);
            //    }
            //}

            Text = string.Empty;
        }


        public async Task WSConnection()
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
            Channel channel = await pusher.SubscribeAsync($"private-chat.1", new SubscriptionEventHandler((object sender) =>
            {
                Console.WriteLine("--- SubscriptionEventHandler: {0}", sender.ToString());
            }));

            channel.Bind("message.sent", (PusherEvent pusherEvent) =>
            {
                Console.WriteLine("--- PusherEvent user_id: {0}, PusherEvent data: {1}", pusherEvent.UserId, pusherEvent.Data);

                ChatPusherResource ChatPusherResource = JsonConvert.DeserializeObject<ChatPusherResource>(pusherEvent.Data);
                Messages.Add(ChatPusherResource.Message);
            });
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
