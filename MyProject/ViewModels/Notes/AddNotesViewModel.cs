using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Models;
using MyProject.Services;
using MyProject.Views;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty(nameof(Note), "NotesCategory")]
    public partial class AddNotesViewModel
    {
        private NotesList _note;
        public NotesList Note
        {
            get => _note;
            set
            {
                if (value != _note)
                {
                    _note = value;
                    RefreshCommand.ExecuteAsync(null);
                }
            }
        }

        [ObservableProperty] private NotesList _notes;
        [ObservableProperty] private string _noteTitle;
        [ObservableProperty] private string _fileName;
        [ObservableProperty] private FileResult _fileResponse;

        private static INotesService _notesService;
        public AddNotesViewModel(INotesService notesService)
        {
            _notesService = notesService;
            Notes = new NotesList();
            FileName = "Select File / Notes";
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            Notes = Note;
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
        private async Task UploadNoteAsync()
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

                        var response = await _notesService.AddNotesAsync(Note.CategoryId, Note.SubjectName, NoteTitle, Note.Semester, FileResponse, CancellationToken.None);
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
