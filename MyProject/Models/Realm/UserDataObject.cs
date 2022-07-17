using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models.Realm
{
    
    public class UserDataObject : RealmObject
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserUsn { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string Semester { get; set; }
        public string Section { get; set; }
        public string Course { get; set; }
        public bool IsAdmin { get; set; }
    }
}
