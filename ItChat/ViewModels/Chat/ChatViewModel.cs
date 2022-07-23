using ItChat.Models;
using ItChat.Services.Http;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Debug.WriteLine("Loaded Chat", Messages);
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

    }
}
