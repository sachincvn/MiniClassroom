using MyProject.ViewModels;

namespace MyProject.Views;

public partial class TimeTableListView : ContentPage
{
	public TimeTableListView(TimeTableListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}