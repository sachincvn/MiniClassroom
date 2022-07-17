using MyProject.ViewModels;

namespace MyProject.Views;

public partial class AddTimeTableView : ContentPage
{
	public AddTimeTableView(AddTimeTableViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}