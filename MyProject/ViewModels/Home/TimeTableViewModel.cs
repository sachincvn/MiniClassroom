using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Enum;
using MyProject.Extensions;
using MyProject.Models;
using MyProject.Models.Realm;
using MyProject.Services;
using MyProject.Views;
using Realms;
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
    [QueryProperty(nameof(TimeTables), "TimeTables")]
    [QueryProperty(nameof(IsAdmin), "IsAdmin")]
    public partial class TimeTableViewModel
    {

        private TimeTables _timeTables;
        public TimeTables TimeTables
        {
            get => _timeTables;
            set
            {
                if (value != null)
                    {
                        if (value.Semester != null && value.Section != null)
                        {
                            IsDataLoading = true;
                            _timeTables = value;
                            TimeTableCollection.Clear();
                            Semester = value.Semester;
                            Section = value.Section;
                            initializeDays().AwaitTask();
                        }
                        else
                        {
                        TimeTableCollection.Clear();
                        dataFromRealm().AwaitTask();
                        }
                    }
                    else
                    {
                    TimeTableCollection.Clear();
                    dataFromRealm().AwaitTask();
                    }
            }
        }

        [ObservableProperty] private ObservableCollection<TimeTableDay> _weekDaysCollection;
        [ObservableProperty] private ObservableCollection<TimeTable> _timeTableCollection;

        [ObservableProperty] private string _semester;
        [ObservableProperty] private string _section;

        [ObservableProperty] private bool _isDataLoading;
        [ObservableProperty] private bool _isAdmin;
        [ObservableProperty] private DayOfWeek _selectedDayOfWeek;
        [ObservableProperty] private UserDataObject _userData;

        private readonly Realms.Realm realmDb;
        private static ITimeTableService _timeTableService;

        public TimeTableViewModel(ITimeTableService timeTableService)
        {
            _timeTableService = timeTableService;
            WeekDaysCollection = new ObservableCollection<TimeTableDay>();
            TimeTableCollection = new ObservableCollection<TimeTable>();
            UserData = new UserDataObject();
            realmDb = Realms.Realm.GetInstance();
            TimeTables = new TimeTables();
        }

        private async Task dataFromRealm()
        {
            IsDataLoading = true;
            var contactsFromDB = realmDb.All<UserDataObject>();
            foreach (var item in contactsFromDB)
            {
                UserData = item;
            }
            Semester = UserData.Semester;
            Section = UserData.Section;
            SelectedDayOfWeek = DayOfWeek.Sunday;
            await initializeDays();
        }

        private async Task initializeDays()
        {
            for (int i = 0; i <= 6; i++)
            {
                WeekDaysCollection.Add(new TimeTableDay()
                {
                    IsSelected = i==0 ? true : false,
                    WeekDay = (DayOfWeek)i
                });
            }
            await Task.Delay(500);
            await TimeTable(SelectedDayOfWeek);
            WeekDaysCollection.FirstOrDefault(i => i.IsSelected).IsSelected = false;
            WeekDaysCollection.FirstOrDefault(i => i.WeekDay == SelectedDayOfWeek).IsSelected = true;

            IsDataLoading = false;
        }

        [RelayCommand]
        private async Task ChangeTimeTableDayAsync(TimeTableDay timeTableModel)
        {
            SelectedDayOfWeek = timeTableModel.WeekDay;
            WeekDaysCollection.FirstOrDefault(i => i.IsSelected).IsSelected = false;
            WeekDaysCollection.FirstOrDefault(i => i.WeekDay == timeTableModel.WeekDay).IsSelected = true;
            
            TimeTableCollection.Clear();
            IsDataLoading = true;
            await TimeTable(timeTableModel.WeekDay);
        }

        private async Task TimeTable(DayOfWeek dayOfWeek)
        {
            TimeTableCollection.Clear();
            try
            {
                var response = await _timeTableService.TimeTableAsync(dayOfWeek.ToString(), Semester, Section, CancellationToken.None);
                var result = JsonSerializer.Deserialize<TimeTableModel>(response.Content);
                switch (result.status)
                {
                    case "200":
                        TimeTableCollection = new ObservableCollection<TimeTable>(result.TimeTable);
                        IsDataLoading = false;
                        break;
                    case "201":
                        await Toast.Make("No Data Present! ").Show();
                        IsDataLoading = false;
                        break;
                    default:
                        await Toast.Make("Something Went Wrong! ").Show();
                        IsDataLoading = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                await Toast.Make("Something Went Wrong! ").Show();
                IsDataLoading = false;
            }
        }

        [RelayCommand]
        private async Task AddTimeTableAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddTimeTableView),false,new Dictionary<string, object>()
            {
                {"Semester",Semester},
                {"Section",Section},
                {"TimeTableId",TimeTables.TimeTableId},
                {"WeekDay",SelectedDayOfWeek.ToString()},
            });
        }
    }
}
