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
    public partial class AllAssignmentsViewModel
    {
        [ObservableProperty]
        private ObservableCollection<AssignmentDetailModel> _assignmentCollection;

        [ObservableProperty] private bool _isDataLoading;

        private readonly IAssignmentService _assignmentService;
        public AllAssignmentsViewModel(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            AssignmentCollection = new ObservableCollection<AssignmentDetailModel>();
            IsDataLoading = true;

            initializeAssignments().AwaitTask();
        }

        private async Task initializeAssignments()
        {
            try
            {
                var response = await _assignmentService.AllAssignmentAsync(CancellationToken.None);
                var result = JsonSerializer.Deserialize<AssignmentsModel>(response.Content);
                switch (result.status)
                {
                    case "200":
                        AssignmentCollection.Clear();
                        //AssignmentCollection = new ObservableCollection<AssignmentDetailModel>(result.Assignments);
                        foreach (var item in result.Assignments)
                        {
                            AssignmentCollection.Add(new AssignmentDetailModel(item.AssignmentId, item.SubjectName, item.AssignmentQuestion, item.AssignedOn, item.DueDate, item.AssignmentMarks, item.AssignmentStatus == "0" ? "Pending" : "Completed", item.AssignmentFile, item.SubjectImage, item.Semester, item.StudentName, item.StudentUsn, item.IsSubmitted, item.id));
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
            AssignmentCollection.Clear();
            IsDataLoading = true;
            initializeAssignments().AwaitTask();
        }

        [RelayCommand]
        private async Task SubmitedAssignmentAsync(AssignmentDetailModel assignmentDetail)
        {
            await Shell.Current.GoToAsync(nameof(SubmitedAssignmentsView), new Dictionary<string, object>
            {
                { "AssignmentModel" ,assignmentDetail }
            });
        }
    }
}
