using MyProject.ViewModels;

namespace MyProject.Views;

public partial class AddAssignmentView : ContentPage
{
	public AddAssignmentView(AddAssignmentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}