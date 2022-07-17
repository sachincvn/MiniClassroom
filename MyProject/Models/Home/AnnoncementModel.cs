using CommunityToolkit.Mvvm.ComponentModel;
using MyProject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Announcement
    {
        public string AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }
        public string Category { get; set; }

        public Announcement(string announcementId, string title, string description, string dateTime, string category)
        {
            AnnouncementId = announcementId;
            Title = title;
            Description = description;
            DateTime = dateTime;
            Category = category;
        }
    }

    public class AnnoncementModel
    {
        public List<Announcement> announcement { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }


}
