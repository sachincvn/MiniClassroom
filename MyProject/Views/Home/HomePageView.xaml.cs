using MyProject.ViewModels;

namespace MyProject.Views;

public partial class HomePageView : ContentPage
{
	public HomePageView(HomePageViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void FlyoutMenuClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }
}