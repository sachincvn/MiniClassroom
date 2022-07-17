using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Extensions;
using MyProject.Models;
using MyProject.Services;
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
    public partial class ManageEventsViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Announcement> _annoncementCollection;

        [ObservableProperty] private bool _isDataLoading;
        [ObservableProperty]  private string _events;
        [ObservableProperty] private string _title;

        private readonly IAnnouncementService _announcementService;
        public ManageEventsViewModel(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
            AnnoncementCollection = new ObservableCollection<Announcement>();
            Events = "Events";
            Title = "Annoncements";
            IsDataLoading = true;
            initializeAnnouncment().AwaitTask();
        }

        private async Task initializeAnnouncment()
        {
            Title = "Events";
            try
            {
                var response = await _announcementService.FilterAnnouncementAsync(Events, CancellationToken.None);
                var result = JsonSerializer.Deserialize<AnnoncementModel>(response.Content);
                switch (result.status)
                {
                    case "200":
                        AnnoncementCollection.Clear();
                        AnnoncementCollection = new ObservableCollection<Announcement>(result.announcement);
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
            IsDataLoading = false;
        }


        [RelayCommand]
        private async Task RefreshAsync()
        {
            AnnoncementCollection.Clear();
            IsDataLoading = true;
            await initializeAnnouncment();
        }
    }
}
