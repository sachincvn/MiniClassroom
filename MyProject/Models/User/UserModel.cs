using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{

    public class UserLogin
    {
        public UserModel user { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }

    public class UserModel
    {
        public string id { get; set; }
        public string usn { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email_address { get; set; }
        public string password { get; set; }
        public string semester { get; set; }
        public string section { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public string mobile_number { get; set; }
        public string parents_mobile { get; set; }
        public string course { get; set; }
        public string gender { get; set; }
        public string isAdmin { get; set; }

        public UserModel(string id, string usn, string first_name, string last_name, string email_address, string password, string semester, string section, string dob, string address, string mobile_number, string parents_mobile, string course, string gender,string isAdmin)
        {
            this.id = id;
            this.usn = usn;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email_address = email_address;
            this.password = password;
            this.semester = semester;
            this.section = section;
            this.dob = dob;
            this.address = address;
            this.mobile_number = mobile_number;
            this.parents_mobile = parents_mobile;
            this.course = course;
            this.gender = gender;
            this.isAdmin = isAdmin;
        }

        public UserModel()
        {

        }
    }
}
