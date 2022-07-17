using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Extensions;
using MyProject.Models;
using MyProject.Models.Realm;
using MyProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("AssignmentModel", "AssignmentModel")]
    [QueryProperty("UserDataObject", "UserDataObject")]
    public partial class AssignmentDetailViewModel
    {
        [ObservableProperty] private AssignmentDetailModel _assignmentModel;
        [ObservableProperty] private UserDataObject _userDataObject;
        [ObservableProperty] private bool _isDataLoading;
        [ObservableProperty] private string _fileName;
        [ObservableProperty] private FileResult _fileResponse;
        private static IAssignmentService _assignmentService;
        public AssignmentDetailViewModel(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            AssignmentModel = new AssignmentDetailModel();
            UserDataObject = new UserDataObject();
            IsDataLoading = false;
            FileName = "Attach Your Assignment";
        }


        [RelayCommand]
        private async Task UploadFileAsync()
        {
            var status = await Permissions.RequestAsync<Permissions.StorageRead>();
            if (status == PermissionStatus.Granted)
            {
                try
                {
                    FileResponse = await FilePicker.Default.PickAsync();
                    if (FileResponse != null)
                    {
                        FileName = FileResponse.FileName;
                    }
                }
                catch (Exception ex)
                {
                    await Toast.Make("Something Went Wrong !").Show();
                }
            }
        }

        [RelayCommand]
        private async Task UploadAssignmentAsync()
        {
            if (FileResponse != null)
            {
                try
                {
                    if (FileResponse.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                           FileResponse.FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase) ||
                           FileResponse.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                           FileResponse.FileName.EndsWith("pdf", StringComparison.OrdinalIgnoreCase) ||
                           FileResponse.FileName.EndsWith("xlxs", StringComparison.OrdinalIgnoreCase) ||
                           FileResponse.FileName.EndsWith("doc", StringComparison.OrdinalIgnoreCase))
                    {

                        var response = await _assignmentService.SubmitAssignmentAsync(AssignmentModel.id,FileResponse,"1", CancellationToken.None);
                        var result = JsonSerializer.Deserialize<AddNoteResponse>(response.Content);
                        switch (result.status)
                        {
                            case 200:
                                await Shell.Current.GoToAsync("..", false);
                                break;
                            case 201:
                                await Toast.Make(result.message).Show();
                                break;
                            case 202:
                                await Toast.Make(result.message).Show();
                                break;
                            case 203:
                                await Toast.Make(result.message).Show();
                                break;
                            case 205:
                                await Toast.Make(result.message).Show();
                                break;
                            default:
                                await Toast.Make("Something Went Wrong !").Show();
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Toast.Make("Something Went Wrong !").Show();
                }
            }
            else
            {
                await Toast.Make("Please select the file to submit !").Show();
            }
        }

    }

}
