using MyProject.ViewModels;

namespace MyProject.Views;

public partial class StudentAssignmentDetail : ContentPage
{
	public StudentAssignmentDetail(StudentAssignmentDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}