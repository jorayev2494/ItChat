using ItChat.Models;
using ItChat.Services.Http;
using ItChat.Views.Auth;
using ItChat.Views.Chat;
using ItChat.Views.Profile;
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
        Routing.RegisterRoute("/profile", typeof(ProfileShowPage));
        Routing.RegisterRoute("/profile/edit", typeof(ProfileEditPage));

        Page();
        InitializeComponent();
    }

    private async Task Page()
    {
        base.OnAppearing();

        //SecureStorage.Default.RemoveAll();
        string acessToken = await SecureStorage.Default.GetAsync("access_token");
        string refreshToken = await SecureStorage.Default.GetAsync("refresh_token");

        if (string.IsNullOrEmpty(acessToken) && string.IsNullOrEmpty(refreshToken))
        {
            await Current.GoToAsync("/login");
        }
    }

    public void ServerValidationExceptions()
    {
        MessagingCenter.Subscribe<Shell, Dictionary<string, string[]>>(this, "validationException", (Shell sender, Dictionary<string, string[]> data) => 
        {
            int index = 0;
            string msgs = string.Empty;

            foreach (KeyValuePair<string, string[]> error in data)
            {
                string msg = error.Value[index];

                for (int i = 0; i < error.Value.Length; i++)
                {

                    if (index == 0)
                    {
                        msgs = msg;
                        continue;
                    }

                    msgs += msg;
                }

                index++;
            }
            
            Shell.Current.DisplayAlert("Validation exception", msgs, "Ok");
        });
    }
}
