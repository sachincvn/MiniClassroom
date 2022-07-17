using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Enum;
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
    public partial class AddAnnoncementViewModel
    {
        [ObservableProperty] private string _annoncementTitle;
        [ObservableProperty] private string _annoncementDescription;
        [ObservableProperty] private Category _selectedCategory;
        [ObservableProperty] private ObservableCollection<Category> _annoncementCategory;

        private static IAnnouncementService _announcementService;
        public AddAnnoncementViewModel(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
            AnnoncementCategory = new ObservableCollection<Category>();
            InitializePage();
        }

        private void InitializePage()
        {
            AnnoncementCategory.Add(Category.Exam);
            AnnoncementCategory.Add(Category.Fee);
            AnnoncementCategory.Add(Category.Holiday);
            AnnoncementCategory.Add(Category.Events);
            AnnoncementCategory.Add(Category.Other);
        }

        [RelayCommand]
        private async Task AddAnnouncementAsync()
        {
            if (!string.IsNullOrEmpty(AnnoncementTitle) && !string.IsNullOrEmpty(SelectedCategory.ToString()) && !string.IsNullOrEmpty(AnnoncementDescription))
            {
                try
                {
                    var response = await _announcementService.AddAnnouncementAsync(Guid.NewGuid().ToString(), AnnoncementTitle, AnnoncementDescription, SelectedCategory.ToString(), DateTime.Now.ToString("dd/yyyy/MM hh:mm tt"), CancellationToken.None);
                    var result = JsonSerializer.Deserialize<Response>(response.Content);

                    if (result.status == "200")
                    {
                        await Toast.Make("Announcement Added Seccessfully ! ").Show();
                        AnnoncementDescription = "";
                        AnnoncementTitle = "";
                    }
                    else
                    {
                        await Toast.Make("Something Went Wrong ! ").Show();
                        AnnoncementDescription = "";
                        AnnoncementTitle = "";
                    }
                }
                catch (Exception ex)
                {
                    await Toast.Make("Something Went Wrong ! ").Show();
                }
            }
            else
            {
                await Toast.Make("Please Enter The Above Details ! ").Show();
            }
        }
    }
}
