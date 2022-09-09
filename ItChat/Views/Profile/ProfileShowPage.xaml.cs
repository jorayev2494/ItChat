using ItChat.ViewModels.Profile;

namespace ItChat.Views.Profile;

public partial class ProfileShowPage : ContentPage
{

	private ProfileShowViewModel viewModel
    {
		get => base.BindingContext as ProfileShowViewModel;
		set => base.BindingContext = value;
    }

    public ProfileShowPage()
	{
        viewModel = new ProfileShowViewModel();

        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //await viewModel.LoadProfileAsync();
    }
}