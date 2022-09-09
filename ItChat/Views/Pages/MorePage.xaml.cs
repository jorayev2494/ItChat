using ItChat.ViewModels.Pages;

namespace ItChat.Views.Pages;

public partial class MorePage : ContentPage
{

	private MoreViewModel viewModel
    {
		get => base.BindingContext as MoreViewModel;
		set => base.BindingContext = value;
    }

	public MorePage()
	{
		this.viewModel = new MoreViewModel();
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await viewModel.LoadProfile();
    }
}