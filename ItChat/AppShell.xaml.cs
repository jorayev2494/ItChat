using ItChat.Models;
using ItChat.Services.Http;
using ItChat.Views.Auth;
using ItChat.Views.Chat;
using System.Collections.ObjectModel;

namespace ItChat;

public partial class AppShell : Shell
{
    public AppShell()
    {
        Routing.RegisterRoute("/login", typeof(LoginPage));

        Page();

        Routing.RegisterRoute("/code", typeof(CodePage));
        Routing.RegisterRoute("/chat", typeof(ChatPage));

        InitializeComponent();
    }

    private async Task Page()
    {
        base.OnAppearing();

        //SecureStorage.Default.Remove("access_token");
        string acessToken = await SecureStorage.Default.GetAsync("access_token");

        if (acessToken == null)
        {
            await Current.GoToAsync("/login");
        }
    }
}
