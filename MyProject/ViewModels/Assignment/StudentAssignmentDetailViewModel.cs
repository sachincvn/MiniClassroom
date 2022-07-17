using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Models;
using MyProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("AssignmentModel", "AssignmentModel")]
    public partial class StudentAssignmentDetailViewModel
    {
        [ObservableProperty] private AssignmentDetailModel _assignmentModel;
        [ObservableProperty] private bool _isDataLoading;
        [ObservableProperty] private string _assignmentMarks;

        private readonly IAssignmentService _assignmentService;

        public StudentAssignmentDetailViewModel(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            AssignmentModel = new AssignmentDetailModel();
        }


        [RelayCommand]
        private async Task DownloadAssignmentAsync()
        {
            try
            {
                var assignmentUrl = $"{Constants.BaseUrl}api/assignments/{AssignmentModel.AssignmentFile}";
                Uri uri = new Uri(assignmentUrl);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await Toast.Make("Something Went Wrong! ").Show();
            }
        }

        [RelayCommand]
        private async Task MarksAsCompleteAsync()
        {
            if (string.IsNullOrEmpty(AssignmentMarks))
            {
                await Toast.Make("Please Enter The Marks").Show();
            }
            else
            {
                try
                {
                    var response = await _assignmentService.UpdateAssignmentAsync(AssignmentModel.id, AssignmentMarks, CancellationToken.None);
                    var result = JsonSerializer.Deserialize<Response>(response.Content);
                    if (result.status == "200")
                    {
                        await Toast.Make("Updated..!").Show();
                    }
                    else
                    {
                        await Toast.Make("Something is wrong !").Show();

                    }
                }
                catch (Exception ex)
                {
                    await Toast.Make("Something is wrong !").Show();
                }
            }
        }
    }
}
