using MyProject.ViewModels;

namespace MyProject.Views;

public partial class AddTimeTableClassView : ContentPage
{
	public AddTimeTableClassView(AddTimeTableClassViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}