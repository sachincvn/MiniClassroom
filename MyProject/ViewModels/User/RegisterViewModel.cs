using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Models;
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
    public partial class RegisterViewModel
    {

        [ObservableProperty] private bool _isLoading;
        [ObservableProperty] private string _firstName;
        [ObservableProperty] private string _lastName;
        [ObservableProperty] private string _usn;
        [ObservableProperty] private string _course;
        [ObservableProperty] private string _emailAddress;
        [ObservableProperty] private string _password;
        [ObservableProperty] private int _semester;
        [ObservableProperty] private char _section;
        [ObservableProperty] private DateTime _dob;
        [ObservableProperty] private string _gender;
        [ObservableProperty] private string _mobileNumber;
        [ObservableProperty] private string _parentMobile;
        [ObservableProperty] private string _address;

        private readonly IUserService _userService;
        public RegisterViewModel(IUserService userService)
        {
            _userService = userService;
        }

        [RelayCommand]
        private async Task RegisterUserAsync()
        {
            IsLoading = true;

            try
            {
                var response = await _userService.AddUserAsync(Guid.NewGuid(), Usn, FirstName, LastName, EmailAddress,
                Password, Semester, Section, Dob, Address, MobileNumber, ParentMobile, Course, Gender, CancellationToken.None);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonSerializer.Deserialize<Response>(response.Content);
                    IsLoading = false;
                    switch (result.status)
                    {
                        case "200": await Toast.Make("Registered Successfully!").Show();
                            await Shell.Current.GoToAsync(nameof(LoginPageView));
                            break;
                        case "201": await Toast.Make("User already exits!").Show();
                            break;
                        default: await Toast.Make("Something Went Wrong !").Show();
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
                await Toast.Make("Server Error, Try Again!").Show();
            }
        }

        [RelayCommand]
        private async Task ExistingUserAsync()
        {
            await Shell.Current.GoToAsync(nameof(LoginPageView));
        }
    }
}
