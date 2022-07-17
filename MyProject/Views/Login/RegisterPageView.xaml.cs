using MyProject.ViewModels;

namespace MyProject.Views;

public partial class RegisterPageView : ContentPage
{
	public RegisterPageView(RegisterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}