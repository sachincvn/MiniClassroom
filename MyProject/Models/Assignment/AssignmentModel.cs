using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    [INotifyPropertyChanged]
    public partial class AssignmentModel
    {
        [ObservableProperty]
        private string _subjectName;

        [ObservableProperty]
        private DateTime _postedDateTime;

        [ObservableProperty]
        private string _assignmentQuestion;

        [ObservableProperty]
        private bool _isPending;

        [ObservableProperty]
        private string _subjectImage;
    }

    public class AssignmentDetailModel
    {
        public string AssignmentId { get; set; }
        public string SubjectName { get; set; }
        public string AssignmentQuestion { get; set; }
        public string AssignedOn { get; set; }
        public string DueDate { get; set; }
        public string AssignmentMarks { get; set; }
        public string AssignmentStatus { get; set; }
        public string AssignmentFile { get; set; }
        public string SubjectImage { get; set; }
        public string Semester { get; set; }
        public string StudentName { get; set; }
        public string StudentUsn { get; set; }
        public string IsSubmitted { get; set; }
        public string id { get; set; }

        public AssignmentDetailModel(string assignmentId, string subjectName, string assignmentQuestion, string assignedOn, string dueDate, string assignmentMarks, string assignmentStatus, string assignmentFile, string subjectImage, string semester, string studentName, string studentUsn, string isSubmitted,string id)
        {
            AssignmentId = assignmentId;
            SubjectName = subjectName;
            AssignmentQuestion = assignmentQuestion;
            AssignedOn = assignedOn;
            DueDate = dueDate;
            AssignmentMarks = assignmentMarks;
            AssignmentStatus = assignmentStatus;
            AssignmentFile = assignmentFile;
            SubjectImage = subjectImage;
            Semester = semester;
            StudentName = studentName;
            StudentUsn = studentUsn;
            IsSubmitted = isSubmitted;
            this.id = id;
        }

        public AssignmentDetailModel()
        {

        }
    }

    public class AssignmentsModel
    {
        public List<AssignmentDetailModel> Assignments { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }


}
