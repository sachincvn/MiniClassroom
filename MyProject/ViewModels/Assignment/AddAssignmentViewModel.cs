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
    public partial class AddAssignmentViewModel
    {
        [ObservableProperty] private string _assignmentSubject;
        [ObservableProperty] private string _assignmentQuestion;
        [ObservableProperty] private DateTime _assignmentDeadline;
        [ObservableProperty] private string _subjectImage;
        [ObservableProperty] private string _semester;

        private static IAssignmentService _assignmentService;
        public AddAssignmentViewModel(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            AssignmentDeadline = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
        }

        [RelayCommand]
        private async Task AddAssignmentAsync()
        {
            if (string.IsNullOrEmpty(AssignmentSubject) && string.IsNullOrEmpty(AssignmentQuestion) || string.IsNullOrEmpty(AssignmentDeadline.ToString()) && string.IsNullOrEmpty(SubjectImage) && string.IsNullOrEmpty(Semester))
            {
                await Toast.Make("Please Enter The Above Details !..").Show();
            }
            else
            {
                try
                {
                    var response = await _assignmentService.AddAssignmentAsync(Semester,AssignmentSubject,SubjectImage,AssignmentQuestion,AssignmentDeadline.ToString("dd/MM/yyyy"),CancellationToken.None);

                    var result = JsonSerializer.Deserialize<Response>(response.Content);
                    if (result.status == "200")
                    {
                        await Toast.Make("Announcement Added Seccessfully ! ").Show();
                        AssignmentSubject = "";
                        AssignmentQuestion = "";
                        SubjectImage = "";
                        Semester = "";
                    }
                    else
                    {
                        await Toast.Make("Something Went Wrong ! ").Show();
                    }
                }
                catch (Exception ex)
                {
                    await Toast.Make("Something Went Wrong ! ").Show();
                }
            }
        }
    }
}
