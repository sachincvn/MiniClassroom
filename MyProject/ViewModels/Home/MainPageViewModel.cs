using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using MyProject.Extensions;
using MyProject.Models;
using MyProject.Models.Realm;
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
    public partial class MainPageViewModel
    {
        [ObservableProperty] private string _userId;
        [ObservableProperty] private string _emailAddress;
        [ObservableProperty] private string _userPassword;
        [ObservableProperty] private bool _isLoading;
        [ObservableProperty] private bool _isNewData;


        private readonly Realms.Realm realmDb;
        private readonly IUserService _userService;

        public MainPageViewModel(IUserService userService)
        {
            _userService = userService;
            realmDb = Realms.Realm.GetInstance();
            checkUserLogedin();
        }

        private async void checkUserLogedin()
        {
            try
            {
                var userdata = realmDb.All<UserDataObject>();
                foreach (var item in userdata)
                {
                    UserId = item.UserId;
                    EmailAddress = item.UserEmail;
                    UserPassword = item.UserPassword;
                }

                if (!string.IsNullOrEmpty(UserId))
                {
                    IsNewData = false;
                    UserLogin().AwaitTask();
                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(LoginPageView));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.GoToAsync(nameof(LoginPageView));
            }
        }

        private async Task UserLogin()
        {
            try
            {
                var response = await _userService.UserLoginAsync(EmailAddress, UserPassword, CancellationToken.None);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonSerializer.Deserialize<UserLogin>(response.Content);
                    switch (result.status)
                    {
                        case "200":
                            await Shell.Current.GoToAsync($"//{nameof(HomePageView)}", true, new Dictionary<string, object>()
                            {
                                { "UserModel", result.user},
                                { "IsAdmin", result.user.isAdmin == "1" ? true : false},
                            });
                            break;
                        default:
                            await Shell.Current.GoToAsync(nameof(LoginPageView));
                            break;
                    }

                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(LoginPageView));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.GoToAsync(nameof(LoginPageView));
            }
        }
    }
}
