using MyProject.Services;
using MyProject.ViewModels;
using MyProject.Views;

namespace MyProject;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.RegisterServices()
			.RegisterView()
            .RegisterViewModel()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		return builder.Build();
	}

    public static MauiAppBuilder RegisterView(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<HomePageView>();
		builder.Services.AddSingleton<AddAnnoncementView>();
		builder.Services.AddSingleton<ManageAssignmentView>();
		builder.Services.AddSingleton<AddAssignmentView>();
		builder.Services.AddSingleton<AssignmentDetailView>();
		builder.Services.AddSingleton<AllAssignmentView>();
		builder.Services.AddSingleton<SubmitedAssignmentsView>();
		builder.Services.AddSingleton<StudentAssignmentDetail>();
		builder.Services.AddSingleton<TimeTableView>();
		builder.Services.AddSingleton<AddTimeTableView>();
		builder.Services.AddSingleton<TimeTableListView>();
		builder.Services.AddSingleton<AddTimeTableClassView>();
		builder.Services.AddSingleton<NotesView>();
		builder.Services.AddSingleton<AddNotesView>();
		builder.Services.AddSingleton<DownloadNotesView>();
		builder.Services.AddSingleton<RegisterPageView>();
		builder.Services.AddSingleton<LoginPageView>();
		builder.Services.AddSingleton<UserProfilePageView>();
		builder.Services.AddSingleton<ManageAnnoncementView>();
		builder.Services.AddSingleton<ManageEventsView>();
		builder.Services.AddSingleton<AdminDashboardView>();

        return builder;
    }

    public static MauiAppBuilder RegisterViewModel(this MauiAppBuilder builder)
    {
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<HomePageViewModel>();
		builder.Services.AddSingleton<AddAnnoncementViewModel>();
		builder.Services.AddSingleton<ManageAssignmentViewModel>();
		builder.Services.AddSingleton<AddAssignmentViewModel>();
		builder.Services.AddSingleton<AllAssignmentsViewModel>();
		builder.Services.AddSingleton<SubmittedAssignmentsViewModel>();
		builder.Services.AddSingleton<StudentAssignmentDetailViewModel>();
        builder.Services.AddSingleton<AssignmentDetailViewModel>();
		builder.Services.AddSingleton<TimeTableViewModel>();
		builder.Services.AddSingleton<AddTimeTableViewModel>();
		builder.Services.AddSingleton<TimeTableListViewModel>();
		builder.Services.AddSingleton<AddTimeTableClassViewModel>();
		builder.Services.AddSingleton<NotesViewModel>();
		builder.Services.AddSingleton<AddNotesViewModel>();
		builder.Services.AddSingleton<DownloadNotesViewModel>();
		builder.Services.AddSingleton<RegisterViewModel>();
		builder.Services.AddSingleton<UserLoginViewModel>();
		builder.Services.AddSingleton<UserProfileViewModel>();
		builder.Services.AddSingleton<ManageAnnoncementViewModel>();
		builder.Services.AddSingleton<ManageEventsViewModel>();
		builder.Services.AddSingleton<AdminDashboardViewModel>();
		return builder;
    }
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IRealmService, RealmService>();
        builder.Services.AddSingleton<INotesService, NotesService>();
		builder.Services.AddScoped<ITimeTableService, TimeTableService>();
		builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
		builder.Services.AddScoped<IAssignmentService, AssignmentService>();
        return builder;
    }
}
