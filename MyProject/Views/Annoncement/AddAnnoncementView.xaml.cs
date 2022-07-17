using MyProject.ViewModels;

namespace MyProject.Views;

public partial class AddAnnoncementView : ContentPage
{
	public AddAnnoncementView(AddAnnoncementViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}