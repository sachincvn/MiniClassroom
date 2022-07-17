using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Enum;
using MyProject.Models;
using System.Collections.ObjectModel;
using MyProject.Extensions;
using MyProject.Services;
using CommunityToolkit.Maui.Alerts;
using System.Text.Json;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty(nameof(Events),"Events")]
    public partial class ManageAnnoncementViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Announcement> _annoncementCollection;

        [ObservableProperty]
        private bool _isDataLoading;

        private string _events;
        public string Events
        {
            get => _events;
            set
            {
                if (value != _events)
                {
                    _events = value;
                    RefreshCommand.Execute(null);
                }
            }
        }

        [ObservableProperty] private string _title;

        private readonly IAnnouncementService _announcementService;

        public ManageAnnoncementViewModel(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
            AnnoncementCollection = new ObservableCollection<Announcement>();
            Events = Category.Other.ToString();
            Title = "Annoncements";
            IsDataLoading = true;
            initializeAnnouncment().AwaitTask();
        }
        private async Task initializeAnnouncment()
        {
            switch (Events)
            {
                case "Holiday":
                    break;
                case "Events":
                    await FilterAnnouncment(Events);
                    break;
                case "Fee":
                    break;
                case "Other":
                    AllAnnouncment().AwaitTask();
                    break;
                default:
                    AllAnnouncment().AwaitTask();
                    break;
            }
        }

        private async Task FilterAnnouncment(string category)
        {
            Title = "Events";
            try
            {
                var response = await _announcementService.FilterAnnouncementAsync(category,CancellationToken.None);
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

        private async Task AllAnnouncment()
        {
            try
            {
                var response = await _announcementService.AnnouncementAsync(CancellationToken.None);
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
