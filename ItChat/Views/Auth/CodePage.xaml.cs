using ItChat.ViewModels.Auth;

namespace ItChat.Views.Auth;

public partial class CodePage : ContentPage
{

	private CodeViewModel viewModel
    {
		get => BindingContext as CodeViewModel;
		set => BindingContext = value;
    }

    public CodePage()
	{
		viewModel = new CodeViewModel();

		InitializeComponent();
	}
}