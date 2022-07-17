using MyProject.ViewModels;

namespace MyProject.Views;

public partial class LoginPageView : ContentPage
{
	public LoginPageView(UserLoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}