using ItChat.Models;
using ItChat.Services.Http;
using ItChat.Services.Pusher;
using ItChat.Services.Pusher.Resources.Chats;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Media;
using MvvmHelpers;
using Newtonsoft.Json;
using PusherClient;
using System.Collections;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Windows.Input;
using WebSocket4Net.Command;

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

        private Models.Chat? chat;

        public Models.Chat? Chat
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
            if (chat is null)
            {
                return;
            }

            IList<Message> serverMessages = await Http.GetAsync<List<Message>>($"/chats/{chat.Id}/messages");
            Messages = new ObservableCollection<Message>();
            int authId = Convert.ToInt32(await SecureStorage.Default.GetAsync("auth_id"));
            string todayFormat = DateTime.Today.ToString("MM/dd/yyyy");

            foreach (Message msg in serverMessages)
            {
                if (msg.Id == authId) msg.Position = LayoutOptions.Start;
                DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(msg.CreatedAt)).DateTime;
                string createdDate = dateTime.ToString("MM/dd/yyyy");
                string dateTimeFormat = (createdDate == todayFormat) ? "HH:mm" : "MM-dd-yyyy HH:mm";
                msg.CreatedAt = dateTime.ToString(dateTimeFormat); 

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

            object dataMessage = new { text = text };

            Message sentMsg = await Http.PostAsync<Message>($"/chats/{chat.Id}/messages", dataMessage);
            DateTime ress = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(sentMsg.CreatedAt)).DateTime;
            sentMsg.CreatedAt = ress.ToString("HH:mm");
            Messages.Add(sentMsg);
            Text = string.Empty;
        }

        private async Task PickMedia()
        {

            

            //if (! MediaPicker.Default.IsCaptureSupported)
            //{
            //    return;
            //}

            //string mediaType = await Shell.Current.DisplayActionSheet("Media", "Cancel", "Destruction", new string[] { "photo", "video", "take photo", "take video" });

            //FileResult media = null;

            //MediaPickerOptions mediaPickerOptions = new MediaPickerOptions()
            //{
            //};

            //switch (mediaType)
            //{
            //    case "photo":
            //        media = await MediaPicker.PickPhotoAsync(mediaPickerOptions);
            //        break;
            //    case "video":
            //        media = await MediaPicker.PickVideoAsync();
            //        break;
            //    case "take photo":
            //        media = await MediaPicker.CapturePhotoAsync();
            //        break;
            //    case "take video":
            //        media = await MediaPicker.CaptureVideoAsync();
            //        break;
            //    default:
            //        break;
            //}

            //try
            //{
            //    await LoadPhotoAsync(media);
            //}
            //catch (FeatureNotSupportedException fnsEx)
            //{
            //    // Feature is not supported on the device
            //}
            //catch (PermissionException pEx)
            //{
            //    // Permissions not granted
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            //}
        }

        private async Task LoadPhotoAsync(FileResult media)
        {

            if (media == null)
            {
                return;
            }

            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();

            if (!string.IsNullOrEmpty(Text) && !string.IsNullOrWhiteSpace(Text))
            {
                multipartFormDataContent.Add(new StringContent(Text, System.Text.Encoding.UTF8), "text");
            }

            multipartFormDataContent.Add(
                new StreamContent(await media.OpenReadAsync()),
                "medias[]",
                media.FileName
            );

            multipartFormDataContent.Add(new StringContent(chat.Id.ToString(), System.Text.Encoding.UTF8), "chat_id");

            Message sentMsg = await Http.PostAsync<Message>($"/chats/{chat.Id}/messages", multipartFormDataContent);
            DateTime ress = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(sentMsg.CreatedAt)).DateTime;
            sentMsg.CreatedAt = ress.ToString("HH:mm");
            Messages.Add(sentMsg);

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
                Encrypted = true,
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
