using MyProject.ViewModels;

namespace MyProject.Views;

public partial class AdminDashboardView : ContentPage
{
	public AdminDashboardView(AdminDashboardViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}