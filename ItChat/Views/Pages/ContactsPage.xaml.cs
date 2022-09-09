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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.loadContacts();
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Models.User slected = e.CurrentSelection.FirstOrDefault() as Models.User;
        viewModel.SelectedContactCommand?.Execute(slected);
    }
}