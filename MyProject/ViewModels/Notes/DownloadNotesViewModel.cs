using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Extensions;
using MyProject.Models;
using MyProject.Services;
using MyProject.Views;
using RestSharp;
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
    [QueryProperty(nameof(Wrapper), "NotesCategory")]
    public partial class DownloadNotesViewModel 
    {

        private WrapperModel<NotesCategory> _wrapper;
        public WrapperModel<NotesCategory> Wrapper
        {
            get => _wrapper;
            set
            {
                if (value != _wrapper)
                {
                    _wrapper = value;
                    RefreshCommand.ExecuteAsync(null);
                }
                else if (value.Operation == _wrapper.Operation)
                {
                    RefreshCommand.ExecuteAsync(null);
                }
            }
        }

        [ObservableProperty] private string title;
        [ObservableProperty] private string _subjectImage;
        [ObservableProperty] private bool _isDataLoading;
        [ObservableProperty] private NotesList _note;

        [ObservableProperty]
        private ObservableCollection<NotesList> notesListCollection;

        private readonly INotesService _notesService;

        public DownloadNotesViewModel(INotesService notesService)
        {
            NotesListCollection = new ObservableCollection<NotesList>();
            Note = new NotesList(); 
            _notesService = notesService;
            Title = "Download Notes";
            IsDataLoading = true;
        }

        private async Task InitializeNotesAsync()
        {
            try
            {
                var response = await _notesService.NotesListyAsync(Wrapper.Item.CategoryId, CancellationToken.None);
                var result = JsonSerializer.Deserialize<NotesListModel>(response.Content);
                switch (result.status)
                {
                    case "200":
                        NotesListCollection.Clear();
                        NotesListCollection = new ObservableCollection<NotesList>(result.NotesList);
                        Note = NotesListCollection[0];
                        break;
                    case "201":
                        await Toast.Make("No Notes Available !").Show();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            IsDataLoading = false;
        }

        [RelayCommand]
        private async Task DownloadNotesAsync(NotesList notesList)
        {
            try
            {
                Uri uri = new Uri(notesList.NotesUrl);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await Toast.Make("Something Went Wrong! ").Show();
            }
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            NotesListCollection.Clear();
            IsDataLoading = true;
            SubjectImage = Wrapper.Item.SubjectImage;

            switch (Wrapper.Operation)
            {
                case Operation.Add:
                    await InitializeNotesAsync();
                    break;
                case Operation.Update:
                    await InitializeNotesAsync();
                    break;
                case Operation.Delete:
                    await InitializeNotesAsync();
                    break;
                case Operation.Display:
                    await InitializeNotesAsync();
                    break;
                default:
                    await InitializeNotesAsync();
                    break;
            }
        }

        [RelayCommand]
        private async Task UploadNotesAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddNotesView), false,new Dictionary<string, object>(){
                {"NotesCategory" ,Note }
            });
        }
    }
}
