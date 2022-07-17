using MyProject.ViewModels;

namespace MyProject.Views;

public partial class ManageAnnoncementView : ContentPage
{
	public ManageAnnoncementView(ManageAnnoncementViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}