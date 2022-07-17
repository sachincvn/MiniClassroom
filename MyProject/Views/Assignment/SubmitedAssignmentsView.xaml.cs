using MyProject.ViewModels;

namespace MyProject.Views;

public partial class SubmitedAssignmentsView : ContentPage
{
	public SubmitedAssignmentsView(SubmittedAssignmentsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}