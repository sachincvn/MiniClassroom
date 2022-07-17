using MyProject.ViewModels;

namespace MyProject.Views;

public partial class DownloadNotesView : ContentPage
{
	public DownloadNotesView(DownloadNotesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}