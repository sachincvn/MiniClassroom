using MyProject.Models.Realm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IRealmService
    {
        Task<string> AddUserDataAsync(string id, string userName, string userUsn, string userEmail,string userPassword,string semester, string section, string course,bool isAdmin, CancellationToken ct);
        Task<string> DeleteUserDataAsync(UserDataObject userDataObject, CancellationToken ct);
    }
}
