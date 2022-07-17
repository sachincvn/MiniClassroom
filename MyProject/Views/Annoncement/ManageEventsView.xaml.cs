using MyProject.ViewModels;

namespace MyProject.Views;

public partial class ManageEventsView : ContentPage
{
	public ManageEventsView(ManageEventsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}