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
    public partial class TimeTableListViewModel
    {
        [ObservableProperty] private ObservableCollection<TimeTables> timeTableList;
        [ObservableProperty] private bool _isDataLoading;

        private static ITimeTableService _timeTableService;
        public TimeTableListViewModel(ITimeTableService timeTableService)
        {
            _timeTableService = timeTableService;
            timeTableList = new ObservableCollection<TimeTables>();
            IsDataLoading = true;
            initializeTimeTableList().AwaitTask();
        }

        private async Task initializeTimeTableList()
        {
            try
            {
                var response = await _timeTableService.TimeTableListAsync(CancellationToken.None);
                var result = JsonSerializer.Deserialize<TimeTableData>(response.Content);
                if (result.status == "200")
                {
                    TimeTableList = new ObservableCollection<TimeTables>(result.TimeTables);
                    IsDataLoading = false;
                }
                else
                {
                    await Toast.Make("Something Went Wromg !").Show();
                    IsDataLoading = false;
                }
            }
            catch (Exception ex)
            {
                await Toast.Make("Something Went Wromg !").Show();
                IsDataLoading = false;
            }
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            TimeTableList.Clear();
            IsDataLoading = true;
            await initializeTimeTableList();
        }

        [RelayCommand]
        private async Task AddTimeTableAsync(TimeTables timeTables)
        {
            await Shell.Current.GoToAsync(nameof(TimeTableView),false,new Dictionary<string, object>()
            {
                {"TimeTables",timeTables },
                {"IsAdmin",true }
            });
        }

        [RelayCommand]
        private async Task AddTimeTableClassAsync(TimeTables timeTables)
        {
            await Shell.Current.GoToAsync(nameof(AddTimeTableClassView),false);
        }
    }
}
