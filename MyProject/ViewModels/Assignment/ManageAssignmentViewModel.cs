using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Extensions;
using MyProject.Models;
using MyProject.Models.Realm;
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
    public partial class ManageAssignmentViewModel
    {
        [ObservableProperty]
        private ObservableCollection<AssignmentDetailModel> _assignmentCollection;

        [ObservableProperty] private bool _isDataLoading;
        [ObservableProperty] private string _semester;
        [ObservableProperty] private UserDataObject _userData;

        private static Realms.Realm realmDb;

        private readonly IAssignmentService _assignmentService;
        public ManageAssignmentViewModel(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            AssignmentCollection = new ObservableCollection<AssignmentDetailModel>();
            IsDataLoading = true;
            realmDb = Realms.Realm.GetInstance();
            UserData = new UserDataObject();
            initializeAssignments().AwaitTask();
        }

        private async Task initializeAssignments()
        {
            realmDb = Realms.Realm.GetInstance();
            var dataFromLocal = realmDb.All<UserDataObject>();
            foreach (var item in dataFromLocal)
            {
                UserData = item;
            }

            try
            {
                var response = await _assignmentService.AssignmentAsync(UserData.Semester,UserData.UserUsn,CancellationToken.None);
                var result = JsonSerializer.Deserialize<AssignmentsModel>(response.Content);
                switch (result.status)
                {
                    case "200":
                        AssignmentCollection.Clear();
                        //AssignmentCollection = new ObservableCollection<AssignmentDetailModel>(result.Assignments);
                        foreach (var item in result.Assignments)
                        {
                           AssignmentCollection.Add(new AssignmentDetailModel(item.AssignmentId, item.SubjectName, item.AssignmentQuestion, item.AssignedOn, item.DueDate, item.AssignmentMarks, item.AssignmentStatus == "0" ? "Pending" : "Completed", item.AssignmentFile, item.SubjectImage, item.Semester, item.StudentName, item.StudentUsn, item.IsSubmitted,item.id));
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
        private async Task AssignmentDetailAsync(AssignmentDetailModel assignmentDetail)
        {
            if (assignmentDetail is null)
                return;

            await Shell.Current.GoToAsync(nameof(AssignmentDetailView),new Dictionary<string, object>
            {
                { "AssignmentModel" ,assignmentDetail },
                { "UserDataObject",UserData}
            });
        }

    }
}
