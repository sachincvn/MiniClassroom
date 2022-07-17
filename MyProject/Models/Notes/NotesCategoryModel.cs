using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class NotesCategory
    {
        public string CategoryId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectImage { get; set; }
        public int TotalNotes { get; set; }
    }

    public class NotesCategoryModel
    {
        public List<NotesCategory> NotesCategory { get; set; }
        public string status { get; set; }
        public string message { get; set; }

/*        public NotesCategory(List<NotesCategoryModel> notesCategoryModel, string status, string message)
        {
            notesCategoryModel = new List<NotesCategoryModel>();
            NotesCategoryModel = notesCategoryModel;
            this.status = status;
            this.message = message;
        }
        public NotesCategory()
        {

        }*/
    }


    public class AddNoteResponse
    {
        public string message { get; set; }
        public int status { get; set; }
    }



}
