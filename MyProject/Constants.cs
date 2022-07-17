using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    public static class Constants
    {
       // public static string ApiBaseUrl =  "http://192.168.137.1/api/";
       //public static string BaseUrl = "http://192.168.43.152/";

       public static string BaseUrl = "https://assignmentmanagerd.000webhostapp.com/";
       public static string ApiBaseUrl = $"{BaseUrl}api/";

        public static string UserRegister = "register-user.php";
        public static string Userlogin = "login-user.php";
        public static string UserById = "get-user.php";

        public static string NotesCategory = "notes-category.php";
        public static string NotesList = "notes-detail.php";
        public static string AddNotes = "add-notes.php";

        public static string TimeTable = "time-table.php";
        public static string TimeTableList = "timetable-list.php";
        public static string AddTimeTableList = "add-timetable-list.php";
        public static string AddTimeTable = "add-timetable.php";

        public static string Announcement = "announcement.php";
        public static string AddAnnouncement = "add-annoncement.php";
        public static string FilterAnnouncement = "filter-announcement.php";

        public static string Assignments = "assignments.php";
        public static string AllAssignments = "all-assignments.php";
        public static string AddAssignments = "add-assignment.php";
        public static string UpdateAssignments = "update-assignment.php";
        public static string SubmitAssignment = "submit-assignment.php";
        public static string SubmittedAssignment = "submited-assignments.php";
    }
}
