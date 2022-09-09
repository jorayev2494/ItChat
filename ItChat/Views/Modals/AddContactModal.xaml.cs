using ItChat.ViewModels.Modals;

namespace ItChat.Views.Modals;

public partial class AddContactModal : ContentPage
{

	private AddContactModalViewModel viewModel
    {
		get => base.BindingContext as AddContactModalViewModel;
		set => base.BindingContext = value;
    }

    public AddContactModal()
	{
        viewModel = new AddContactModalViewModel();
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.LoadCountriesAsync();
    }
}