using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Models;
using MyProject.Models.Realm;
using MyProject.Services;
using MyProject.Views;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    public partial class UserLoginViewModel
    {
        [ObservableProperty] private string _emailAddress;
        [ObservableProperty] private string _userPassword;
        [ObservableProperty] private bool _isLoading;
        [ObservableProperty] private bool _isNewData;

        private readonly IUserService _userService;
        private readonly IRealmService _realmService;

        public UserLoginViewModel(IUserService userService,IRealmService realmService)
        {
            _userService = userService;
            _realmService = realmService;
            IsNewData = true;
        }


        [RelayCommand]
        private async Task UserLoginAsync()
        {
            IsLoading = true;
            try
            {
                var response = await _userService.UserLoginAsync(EmailAddress, UserPassword,CancellationToken.None);
                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonSerializer.Deserialize<UserLogin>(response.Content);
                    IsLoading = false;
                    switch (result.status)
                    {
                        case "200":
                            await Toast.Make("Login Successfully!").Show();
                            await Shell.Current.GoToAsync($"//{nameof(HomePageView)}", true,new Dictionary<string, object>()
                            {
                                { "UserModel", result.user},
                                { "IsAdmin", result.user.isAdmin == "1" ? true : false},
                            });
                            if (IsNewData)
                                await _realmService.AddUserDataAsync(result.user.id, $"{result.user.first_name} {result.user.last_name}",result.user.usn, result.user.email_address, 
                                    result.user.password,result.user.semester,result.user.section,
                                    result.user.course,result.user.isAdmin=="1" ? true : false, CancellationToken.None);
                            break;
                        case "202":
                            await Toast.Make("User not exits!").Show();
                            break;
                        default:
                            await Toast.Make("Something Went Wrong !").Show();
                            break;
                    }

                }
                else
                {
                    IsLoading = false;
                    await Toast.Make("Server Error, Try Again!").Show();
                }
            }
            catch (Exception ex)
            {
                IsLoading = false;
                await Toast.Make("Something Went Wrong !").Show();
            }
        }

        [RelayCommand]
        private async Task NewUserRegisterAsync()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPageView));
        }
    }
}
