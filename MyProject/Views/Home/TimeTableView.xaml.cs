using MyProject.ViewModels;

namespace MyProject.Views;

public partial class TimeTableView : ContentPage
{
	public TimeTableView(TimeTableViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}