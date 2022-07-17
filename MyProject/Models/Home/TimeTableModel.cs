using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    [INotifyPropertyChanged]

    public partial class TimeTableDay
    {
        [ObservableProperty] private bool _isSelected;
        [ObservableProperty] private DayOfWeek _weekDay;

    }

    public class TimeTableModel
    {
        public List<TimeTable> TimeTable { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }

    public class TimeTable
    {
        public string Id { get; set; }
        public string TimeTableId { get; set; }
        public string Semester { get; set; }
        public string Section { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
        public string WeekDay { get; set; }
    }


    public class TimeTableData
    {
        public List<TimeTables> TimeTables { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }

    public class TimeTables
    {
        public string TimeTableId { get; set; }
        public string Semester { get; set; }
        public string Section { get; set; }
    }

}
