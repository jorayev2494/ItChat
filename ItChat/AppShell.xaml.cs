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
        ServerValidationExceptions();

        Routing.RegisterRoute("/login", typeof(LoginPage));
        Routing.RegisterRoute("/code", typeof(CodePage));
        Routing.RegisterRoute("/chat", typeof(ChatPage));

        Page();
        InitializeComponent();
    }

    private async Task Page()
    {
        base.OnAppearing();

        string acessToken = await SecureStorage.Default.GetAsync("access_token");

        if (acessToken == null)
        {
            await Current.GoToAsync("/login");
        }
    }

    public void ServerValidationExceptions()
    {
        MessagingCenter.Subscribe<Shell, Dictionary<string, string[]>>(this, "validationException", (Shell sender, Dictionary<string, string[]> data) => 
        {
            foreach (KeyValuePair<string, string[]> error in data)
            {
                foreach (string msg in error.Value)
                {
                    Shell.Current.DisplayAlert("Validation exception", msg, "Ok");
                }
            }
        });
    }
}
