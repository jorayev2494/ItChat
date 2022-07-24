using ItChat.ViewModels.Chat;

namespace ItChat.Views.Chat;

public partial class ChatPage : ContentPage
{

	private ChatViewModel viewModel
    {
		get => base.BindingContext as ChatViewModel;
		set => base.BindingContext = value as ChatViewModel;
    }

    public ChatPage()
	{
		viewModel = new ChatViewModel();
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.LoadChat();
    }
}