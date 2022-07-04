using ItChat.ViewModels.Auth;

namespace ItChat.Views.Auth;

public partial class LoginPage : ContentPage
{

	private LoginViewModel viewModel 
	{
		get => this.BindingContext as LoginViewModel;
		set => this.BindingContext = value as LoginViewModel;
    }

    public LoginPage()
	{
		this.viewModel = new LoginViewModel();

		InitializeComponent();
	}
}