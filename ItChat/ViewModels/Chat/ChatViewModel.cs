using ItChat.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Chat
{
    [QueryProperty(nameof(ItChat.Models.Chat), "Chat")]
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

        private ItChat.Models.Chat chat;

        public ItChat.Models.Chat Chat
        {
            get => this.chat;
            set {
                base.SetProperty<ItChat.Models.Chat>(ref chat, value);
            }
        }

        private IList<Message> messages;

        public IList<Message> Messages
        {
            get { return messages; }
            set { base.SetProperty(ref messages, value); }
        }

        public ICommand SendMessageCommand { get; private set; }

        public ChatViewModel()
        {
            Messages = new ObservableCollection<Message>()
            {
                new Message()
                {
                    Id = 1,
                    Text = "Message 1 text message text message text message text message text",
                    Media = null,
                    IsRead = true,
                    Position = LayoutOptions.End,
                    CreatedAt = "23:02"
                },
                new Message()
                {
                    Id = 2,
                    Text = "Message 2 text message text message text message text message text",
                    Media = "https://cdn.winknews.com/wp-content/uploads/20160629104105/50401E00-GISTI.jpg",
                    IsRead = true,
                    Position = LayoutOptions.Start,
                    CreatedAt = "22:02"
                },
                new Message()
                {
                    Id = 3,
                    Text = "Message 3 text message text message text message text message text",
                    Media = null,
                    IsRead = true,
                    Position = LayoutOptions.Start,
                    CreatedAt = "22:20"
                },
                new Message()
                {
                    Id = 4,
                    Text = "Message 4 text message text message text message text message text",
                    Media = "https://cdn.winknews.com/wp-content/uploads/20160629104105/50401E00-GISTI.jpg",
                    IsRead = true,
                    Position = LayoutOptions.End,
                    CreatedAt = "23:02"
                },
                new Message()
                {
                    Id = 5,
                    Text = "Message 5 text message text message text message text message text",
                    Media = "https://ae01.alicdn.com/kf/H9c162bf534444e93b63d27620e9c80bft.jpg?width=800&height=800&hash=1600",
                    IsRead = true,
                    Position = LayoutOptions.Start,
                    CreatedAt = "22:02"
                },
                new Message()
                {
                    Id = 6,
                    Text = "Message 6 text message text message text message text message text",
                    Media = null,
                    IsRead = true,
                    Position = LayoutOptions.End,
                    CreatedAt = "21:20"
                },
                new Message()
                {
                    Id = 7,
                    Text = "Message 7 text message text message text message text message text",
                    Media = "https://ae01.alicdn.com/kf/H9c162bf534444e93b63d27620e9c80bft.jpg?width=800&height=800&hash=1600",
                    IsRead = true,
                    Position = LayoutOptions.Start,
                    CreatedAt = "20:02"
                },
                new Message()
                {
                    Id = 8,
                    Text = "Message 8 text message text message text message text message text",
                    Media = null,
                    IsRead = true,
                    Position = LayoutOptions.End,
                    CreatedAt = "19:02"
                },
                new Message()
                {
                    Id = 9,
                    Text = "Message 9 text message text message text message text message text",
                    Media = "https://www.mycatsitter.co.uk/wp-content/uploads/2020/06/Cat-sitting-Mason.jpg",
                    IsRead = true,
                    Position = LayoutOptions.Start,
                    CreatedAt = "18:20"
                },
                new Message()
                {
                    Id = 10,
                    Text = "Message 10 text message text message text message text message text",
                    Media = null,
                    IsRead = true,
                    Position = LayoutOptions.End,
                    CreatedAt = "17:20"
                }
            };

            SendMessageCommand = new Command(SendMessage, () => text.Length > 2);
        }

        private void SendMessage()
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            Message newMessage = new Message() { Id = messages.Count + 1, Text = text, Media = null, Position = LayoutOptions.End, IsRead = false, CreatedAt = "now" };
            Messages.Add(newMessage);
            Text = string.Empty;
        }

    }
}
