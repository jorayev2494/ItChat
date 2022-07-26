using ItChat.ViewModels.Pages;

namespace ItChat.Views.Pages;

public partial class ContactsPage : ContentPage
{

	private ContactsViewModel viewModel
    {
		get => base.BindingContext as ContactsViewModel;
		set => base.BindingContext = value;
    }

    public ContactsPage()
	{
		viewModel = new ContactsViewModel();
		InitializeComponent();
	}
}