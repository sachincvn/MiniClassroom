using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Models;
using MyProject.Extensions;
using MyProject.Services;
using System.Text.Json;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using MyProject.Views;

namespace MyProject.ViewModels
{
    [INotifyPropertyChanged]
    public partial class NotesViewModel
    {
        [ObservableProperty]
        private ObservableCollection<NotesCategory> _notesCategoryCollection;

        [ObservableProperty] private bool isDataLoading;

        private readonly INotesService _notesService;

        public NotesViewModel(INotesService notesService)
        {
            _notesService = notesService;
            NotesCategoryCollection = new ObservableCollection<NotesCategory>();

            isDataLoading = true;
            initalizeNotesCategory().AwaitTask();
        }

        private async Task initalizeNotesCategory()
        {
            try
            {
                var response = await _notesService.NotesCategoryAsync(CancellationToken.None);
                var result = JsonSerializer.Deserialize<NotesCategoryModel>(response.Content);

                switch (result.status)
                {
                    case "200":
                        NotesCategoryCollection = new ObservableCollection<NotesCategory>(result.NotesCategory);
                        break;
                    case "201":
                        await Toast.Make("No category Available !").Show();
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
        private async Task DownloadNotesAsync(NotesCategory notesCategory)
        {
            if (notesCategory.TotalNotes ==0)
            {
                await Toast.Make($"{notesCategory.SubjectName} Notes are not available right now \n You Can Upload Notes Here !").Show();
            }
            await Shell.Current.GoToAsync(nameof(DownloadNotesView), false, new Dictionary<string, object>()
            {
                    {"NotesCategory",new WrapperModel<NotesCategory>(notesCategory, Operation.Display) }
            });
        }


    }
}
