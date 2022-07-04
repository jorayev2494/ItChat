using ItChat.ViewModels.Auth;

namespace ItChat.Views.Auth;

public partial class CodePage : ContentPage
{

	private CodeViewModel viewModel
    {
		get => this.BindingContext as CodeViewModel;
		set => this.BindingContext = value as CodeViewModel;
    }

    public CodePage()
	{
		this.viewModel = new CodeViewModel();

		InitializeComponent();
	}
}