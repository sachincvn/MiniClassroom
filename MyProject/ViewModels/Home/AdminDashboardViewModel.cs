using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    [QueryProperty(nameof(UserModel), "UserModel")]
    public partial class AdminDashboardViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Announcement> annoncementCollection;

        [ObservableProperty] private string _currentDateTime;
        [ObservableProperty] private string _dayGreeting;

        [ObservableProperty] private AnnoncementModel annoncement;
        [ObservableProperty] private UserModel userModel;

        private readonly IAnnouncementService _announcementService;
        public AdminDashboardViewModel(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
            UserModel = new UserModel();
            annoncementCollection = new ObservableCollection<Announcement>();
            initializeAnnoncement().GetAwaiter();
        }


        private async Task initializeAnnoncement()
        {
            CurrentDateTime = $"{DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy")} {DateTime.Now.ToLocalTime().ToString("hh:mm tt")}";
            int timeOfDay = Convert.ToInt32(DateTime.Now.ToLocalTime().ToString("HH"));
            DayGreeting = timeOfDay >= 0 && timeOfDay < 12 ? "Good Morning" : timeOfDay >= 12 && timeOfDay < 16 ? "Good Afternoon" :
                timeOfDay >= 16 && timeOfDay < 21 ? "Good Evening" : "Good Night";
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
                            if (count == 5)
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
                        AnnoncementCollection.Add(new Announcement(Guid.NewGuid().ToString(), "Opps No Anouncement Available", "Something Went Wrong ?", DateTime.Now.ToString(), "other"));
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
        private async Task AddAnnoncementAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddAnnoncementView));
        }

        [RelayCommand]
        private async Task AddAssignmentAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddAssignmentView));
        }

        [RelayCommand]
        private async Task TimeTableListAsync()
        {
            await Shell.Current.GoToAsync(nameof(TimeTableListView));
        }

        [RelayCommand]
        private async Task CheckAssignmentsAsync()
        {
            await Shell.Current.GoToAsync(nameof(AllAssignmentView));
        }
    }
}
