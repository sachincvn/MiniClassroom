namespace MyProject.Views;

public partial class NotesView : ContentPage
{
	public NotesView(ViewModels.NotesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}