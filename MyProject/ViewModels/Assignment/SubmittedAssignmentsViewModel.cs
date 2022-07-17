using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Extensions;
using MyProject.Models;
using MyProject.Services;
using MyProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("AssignmentModel", "AssignmentModel")]
    public partial class SubmittedAssignmentsViewModel
    {
        [ObservableProperty] private AssignmentDetailModel _assignmentModel;
        [ObservableProperty] private bool _isDataLoading;
        [ObservableProperty] private ObservableCollection<AssignmentDetailModel> _submitedAssignmentCollection;

        private readonly IAssignmentService _assignmentService;
        public SubmittedAssignmentsViewModel(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            AssignmentModel = new AssignmentDetailModel();
            SubmitedAssignmentCollection = new ObservableCollection<AssignmentDetailModel>();
            IsDataLoading = true;
            initializeSubmittedAssignment().AwaitTask();
        }

        private async Task initializeSubmittedAssignment()
        {
            try
            {
                var response = await _assignmentService.SubmittedAssignmentAsync("93299053-b091-4c74-8be6-720196eac3ae", CancellationToken.None);
                var result = JsonSerializer.Deserialize<AssignmentsModel>(response.Content);
                switch (result.status)
                {
                    case "200":
                        SubmitedAssignmentCollection.Clear();
                        //AssignmentCollection = new ObservableCollection<AssignmentDetailModel>(result.Assignments);
                        foreach (var item in result.Assignments)
                        {
                            SubmitedAssignmentCollection.Add(new AssignmentDetailModel(item.AssignmentId, item.SubjectName, item.AssignmentQuestion, item.AssignedOn, item.DueDate, item.AssignmentMarks, item.AssignmentStatus == "0" ? "Pending" : "Completed", item.AssignmentFile, item.SubjectImage, item.Semester, item.StudentName, item.StudentUsn, item.IsSubmitted, item.id));
                        }
                        break;
                    case "201":
                        await Toast.Make("Opps ! No Assignment Availbale ").Show();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                await Toast.Make("Something Went Wrong !").Show();
            }
            IsDataLoading = false;
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            SubmitedAssignmentCollection.Clear();
            IsDataLoading = true;
            initializeSubmittedAssignment().AwaitTask();
        }

        [RelayCommand]
        private async Task UpdateAssignmentAsync(AssignmentDetailModel assignmentDetail)
        {
            await Shell.Current.GoToAsync(nameof(StudentAssignmentDetail), new Dictionary<string, object>
            {
                { "AssignmentModel" ,assignmentDetail }
            });
        }
    }
}
