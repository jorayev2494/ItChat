using ItChat.Models;
using ItChat.ViewModels.Modals;

namespace ItChat.Views.Modals;

public partial class SelectCountryModal : ContentPage
{

	private SelectCountryModalViewModel viewModel
    {
		get => base.BindingContext as SelectCountryModalViewModel;
		set => base.BindingContext = value;
    }

    public SelectCountryModal(IList<Country> countries)
	{
		viewModel = new SelectCountryModalViewModel(countries);

        InitializeComponent();
	}
}