using ItChat.ViewModels.Auth;

namespace ItChat.Views.Auth;

public partial class LoginPage : ContentPage
{

	private LoginViewModel viewModel 
	{
		get => BindingContext as LoginViewModel;
		set => BindingContext = value;
    }

    public LoginPage()
	{
		viewModel = new LoginViewModel();

		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}