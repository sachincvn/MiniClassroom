using MyProject.ViewModels;

namespace MyProject.Views;

public partial class UserProfilePageView : ContentPage
{
	public UserProfilePageView(UserProfileViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}