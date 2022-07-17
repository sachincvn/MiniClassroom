using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    [INotifyPropertyChanged]
    public partial class NotesList
    {
        [ObservableProperty] private string _subjectName;
        [ObservableProperty] private string _categoryId;
        [ObservableProperty] private string _notesTitle;
        [ObservableProperty] private string _notesUrl;
        [ObservableProperty] private string _semester;

        public NotesList(string categoryId,string subjectName,string notesTitle,string notesUrl,string semester)
        {
            CategoryId = categoryId;
            SubjectName = subjectName;
            NotesTitle = notesTitle;
            NotesUrl = notesUrl;
            Semester = semester;
        }
        public NotesList()
        {

        }
    }

    public class NotesListModel
    {
        public List<NotesList> NotesList { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
