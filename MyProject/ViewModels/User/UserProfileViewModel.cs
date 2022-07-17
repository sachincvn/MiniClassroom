using Android.Service.Autofill;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Extensions;
using MyProject.Models;
using MyProject.Models.Realm;
using MyProject.Services;
using MyProject.Views;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    public partial class UserProfileViewModel
    {
        [ObservableProperty] private string userId;
        [ObservableProperty] private string fullName;
        [ObservableProperty] private UserLogin _userData;
        [ObservableProperty] private UserDataObject _userDataObject;

        private readonly Realms.Realm realmDb;
        private static IUserService _userService;
        public UserProfileViewModel(IUserService userService)
        {
            UserData = new UserLogin();
            _userService = userService;
            realmDb = Realms.Realm.GetInstance();
            var userFromDb = realmDb.All<UserDataObject>();
            foreach (var item in userFromDb)
            {
                UserId = item.UserId;
                UserDataObject = item;
            }
            FullName = "Full Name";

            InitializeUserPropfile().AwaitTask();
        }

        private async Task InitializeUserPropfile()
        {
            try
            {
                var response = await _userService.UserByIdAsync(UserId, CancellationToken.None);
                var result = JsonSerializer.Deserialize<UserLogin>(response.Content);
                UserData = result;
                FullName = result.user.first_name + " "+ result.user.last_name;
            }
            catch (Exception ex)
            {
                await Toast.Make("Something Went Wrong !").Show();
            }
        }

        [RelayCommand]
        private async Task UserLogoutAsync()
        {
            try
            {
                using (var db = realmDb.BeginWrite())
                {
                    realmDb.Remove(UserDataObject);
                    db.Commit();
                }
                await Shell.Current.GoToAsync($"{nameof(LoginPageView)}", false);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
