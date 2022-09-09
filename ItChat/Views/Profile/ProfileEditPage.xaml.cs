using ItChat.ViewModels.Profile;

namespace ItChat.Views.Profile;

public partial class ProfileEditPage : ContentPage
{

    private ProfileEditViewModel ViewModel
    {
        get => base.BindingContext as ProfileEditViewModel;
        set => base.BindingContext = value;
    }


    public ProfileEditPage()
	{
        ViewModel = new ProfileEditViewModel();
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.LoadCountries();
    }

    private async void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        Switch @switch = (Switch)sender;
    }
}