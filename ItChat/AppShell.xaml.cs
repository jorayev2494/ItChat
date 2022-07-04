using ItChat.Views.Chat;

namespace ItChat;

public partial class AppShell : Shell
{
    public AppShell()
    {
        Routing.RegisterRoute("/chat", typeof(ChatPage));

        InitializeComponent();
    }
}
