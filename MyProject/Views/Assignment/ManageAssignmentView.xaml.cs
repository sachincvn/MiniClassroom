using MyProject.ViewModels;

namespace MyProject.Views;

public partial class ManageAssignmentView : ContentPage
{
	public ManageAssignmentView(ManageAssignmentViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}