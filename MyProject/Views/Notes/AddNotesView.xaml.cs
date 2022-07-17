using MyProject.ViewModels;

namespace MyProject.Views;

public partial class AddNotesView : ContentPage
{
	public AddNotesView(AddNotesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}