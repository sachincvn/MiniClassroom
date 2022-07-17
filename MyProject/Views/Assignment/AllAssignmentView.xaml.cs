using MyProject.ViewModels;

namespace MyProject.Views;

public partial class AllAssignmentView : ContentPage
{
	public AllAssignmentView(AllAssignmentsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}