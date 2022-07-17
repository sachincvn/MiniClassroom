using MyProject.ViewModels;

namespace MyProject.Views;

public partial class AssignmentDetailView : ContentPage
{
	public AssignmentDetailView(AssignmentDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}