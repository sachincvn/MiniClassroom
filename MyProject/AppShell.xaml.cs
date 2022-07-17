using MyProject.Views;

namespace MyProject;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(HomePageView), typeof(HomePageView));
        Routing.RegisterRoute(nameof(LoginPageView), typeof(LoginPageView));
        Routing.RegisterRoute(nameof(RegisterPageView), typeof(RegisterPageView));
        Routing.RegisterRoute(nameof(ManageAnnoncementView), typeof(ManageAnnoncementView));
        Routing.RegisterRoute(nameof(ManageEventsView), typeof(ManageEventsView));
        Routing.RegisterRoute(nameof(AddAnnoncementView), typeof(AddAnnoncementView));
        Routing.RegisterRoute(nameof(ManageAssignmentView), typeof(ManageAssignmentView));
        Routing.RegisterRoute(nameof(AddAssignmentView), typeof(AddAssignmentView));
        Routing.RegisterRoute(nameof(AllAssignmentView), typeof(AllAssignmentView));
        Routing.RegisterRoute(nameof(SubmitedAssignmentsView), typeof(SubmitedAssignmentsView));
        Routing.RegisterRoute(nameof(AssignmentDetailView), typeof(AssignmentDetailView));
        Routing.RegisterRoute(nameof(StudentAssignmentDetail), typeof(StudentAssignmentDetail));
        Routing.RegisterRoute(nameof(TimeTableView), typeof(TimeTableView));
        Routing.RegisterRoute(nameof(TimeTableListView), typeof(TimeTableListView));
        Routing.RegisterRoute(nameof(AddTimeTableClassView), typeof(AddTimeTableClassView));
        Routing.RegisterRoute(nameof(AddTimeTableView), typeof(AddTimeTableView));
        Routing.RegisterRoute(nameof(NotesView), typeof(NotesView));
        Routing.RegisterRoute(nameof(AddNotesView), typeof(AddNotesView));
        Routing.RegisterRoute(nameof(DownloadNotesView), typeof(DownloadNotesView));
        Routing.RegisterRoute(nameof(DownloadNotesView), typeof(DownloadNotesView));
        Routing.RegisterRoute(nameof(AdminDashboardView), typeof(AdminDashboardView));

    }
}
