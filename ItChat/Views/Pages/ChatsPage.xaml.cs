using ItChat.Models;
using ItChat.ViewModels.Pages;

namespace ItChat.Views.Pages;

public partial class ChatsPage : ContentPage
{

	private ChatsPageViewModel viewModel
    {
		get => base.BindingContext as ChatsPageViewModel;
		set => base.BindingContext = value as ChatsPageViewModel;
    }

    public ChatsPage()
	{
        viewModel = new ChatsPageViewModel();

        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.LoadChataAsync();
    }
}