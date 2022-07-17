using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    [INotifyPropertyChanged]
    public partial class NotesCategoryModell
    {
        [ObservableProperty]
        private string _categoryName;

        [ObservableProperty]
        private string _categoryImage;

        [ObservableProperty]
        private int _totalNotesCount;

        public NotesCategoryModell(string categoryName, string categoryImage, int totalNotesCount)
        {
            CategoryName = categoryName;
            CategoryImage = categoryImage;
            TotalNotesCount = totalNotesCount;
        }
    }
}
