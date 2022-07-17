using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Models;
using MyProject.Services;
using MyProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    public partial class AddTimeTableClassViewModel
    {
        [ObservableProperty] private string _semester;
        [ObservableProperty] private string _section;

        private static ITimeTableService _timeTableService;
        public AddTimeTableClassViewModel(ITimeTableService timeTableService)
        {
            _timeTableService = timeTableService;
        }

        [RelayCommand]
        private async Task AddTimeTableClassAsync()
        {
            if (string.IsNullOrEmpty(Semester) && string.IsNullOrEmpty(Semester))
            {
                await Toast.Make("Please Select The Above Details ! ").Show();
            }
            else
            {
                try
                {
                    var response = await _timeTableService.AddTimeTableListAsync(Semester, Section, CancellationToken.None);

                    var result = JsonSerializer.Deserialize<Response>(response.Content);
                    if (result.status == "200")
                    {
                        await Toast.Make("Added Seccessfully ! ").Show();
                        await Shell.Current.GoToAsync(nameof(TimeTableListView));
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
