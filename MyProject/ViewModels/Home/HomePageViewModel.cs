using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Enum;
using MyProject.Models;
using MyProject.Services;
using MyProject.Views;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty(nameof(UserModel), "UserModel")]
    [QueryProperty(nameof(IsAdmin), "IsAdmin")]
    public partial class HomePageViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Announcement> annoncementCollection;

        [ObservableProperty] private string _currentDateTime;
        [ObservableProperty] private string _dayGreeting;
        [ObservableProperty] private bool _isAdmin;

        [ObservableProperty]
        private AnnoncementModel annoncement;

        [ObservableProperty]
        private UserModel _userModel;

        private  readonly IUserService _userService;
        private  readonly IAnnouncementService _announcementService;


        public HomePageViewModel(IUserService userService,IAnnouncementService announcementService)
        {
            _userService = userService;
            _announcementService = announcementService;

            annoncementCollection = new ObservableCollection<Announcement>();
            UserModel = new UserModel();

            initializeAnnoncement().GetAwaiter();
        }


        private async Task initializeAnnoncement()
        {
            CurrentDateTime = $"{DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy")} {DateTime.Now.ToLocalTime().ToString("hh:mm tt")}";
            int timeOfDay = Convert.ToInt32(DateTime.Now.ToLocalTime().ToString("HH"));
            DayGreeting = timeOfDay >= 0 && timeOfDay < 12 ? "Good Morning" : timeOfDay >= 12 && timeOfDay < 16 ? "Good Afternoon" :
                timeOfDay >= 16 && timeOfDay < 21 ? "Good Evening" :  "Good Night";
            try
            {
                var response = await _announcementService.AnnouncementAsync(CancellationToken.None);
                var result = JsonSerializer.Deserialize<AnnoncementModel>(response.Content);
                switch (result.status)
                {
                    case "200":
                        AnnoncementCollection.Clear();
                        int count = 0;
                        foreach (var item in result.announcement)
                        {
                            if (count==5)
                            {
                                break;
                            }
                            else
                            {
                                AnnoncementCollection.Add(new Announcement(item.AnnouncementId, item.Title, item.Description, item.DateTime, item.Category));
                            }
                            count++;
                        }
                        break;
                    case "201":
                        await Toast.Make("Opps ! No Anouncement Availbale ").Show();
                        AnnoncementCollection.Add(new Announcement(Guid.NewGuid().ToString(), "Opps No Anouncement Available", "Something Went Wrong ?",DateTime.Now.ToString(), "other"));
                        break;
                    default:
                        AnnoncementCollection.Add(new Announcement(Guid.NewGuid().ToString(), "Opps No Anouncement Available", "Something Went Wrong ?", DateTime.Now.ToString(), "other"));
                        break;
                }
            }
            catch (Exception ex)
            {
                AnnoncementCollection.Add(new Announcement(Guid.NewGuid().ToString(), "Opps No Anouncement Available", "Something Went Wrong ?", DateTime.Now.ToString(), "other"));
                await Toast.Make("Something Went Wrong !").Show();
            }
        }


        [RelayCommand]
        private async Task TimeTableAsync()
        {
            await Shell.Current.GoToAsync(nameof(TimeTableView), false,new Dictionary<string, object>()
            {
                {"IsAdmin",IsAdmin }
            });
        }

        [RelayCommand]
        private async Task NotesAsync()
        {
            await Shell.Current.GoToAsync(nameof(NotesView), false);
        }

        [RelayCommand]
        private async Task EventsAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(ManageEventsView)}", false);
        }

        [RelayCommand]
        private async Task AssignmentsAsync()
        {
            await Shell.Current.GoToAsync(nameof(ManageAssignmentView), false);
        }

        [RelayCommand]
        private async Task AdminDashboardAsync()
        {
            await Shell.Current.GoToAsync(nameof(AdminDashboardView), false,new Dictionary<string, object>()
            {
                {"UserModel",UserModel }
            });
        }
    }
}
