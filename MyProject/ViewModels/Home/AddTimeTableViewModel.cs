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
    [QueryProperty(nameof(Semester),"Semester")]
    [QueryProperty(nameof(Section),"Section")]
    [QueryProperty(nameof(TimeTableId), "TimeTableId")]
    [QueryProperty(nameof(SelectedDay),"WeekDay")]
    public partial class AddTimeTableViewModel
    {
        [ObservableProperty] private string _semester;
        [ObservableProperty] private string _section;
        [ObservableProperty] private string _selectedDay;
        [ObservableProperty] private string _timeTableId;

        [ObservableProperty] private string _subject;
        [ObservableProperty] private string _teacher;
        [ObservableProperty] private string _startTime;
        [ObservableProperty] private string _endTime;

        private static ITimeTableService _timeTableService;
        public AddTimeTableViewModel(ITimeTableService timeTableService)
        {
            _timeTableService = timeTableService;
        }

        [RelayCommand]
        private async Task AddTimeTableAsync()
        {
            if (string.IsNullOrEmpty(Semester) || string.IsNullOrEmpty(Section) || string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Teacher) || string.IsNullOrEmpty(StartTime) || string.IsNullOrEmpty(EndTime) || string.IsNullOrEmpty(SelectedDay))
            {
                await Toast.Make("Please Select The Above Details ! ").Show();
            }
            else
            {
                try
                {
                    var response = await _timeTableService.AddTimeTableAsync(TimeTableId,Subject,Teacher,StartTime,EndTime,Semester,Section,SelectedDay, CancellationToken.None);
                    var result = JsonSerializer.Deserialize<Response>(response.Content);
                    if (result.status == "200")
                    {
                        await Toast.Make("Added Seccessfully ! ").Show();
                        Subject = "";  Teacher = ""; StartTime = ""; EndTime = "";
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
