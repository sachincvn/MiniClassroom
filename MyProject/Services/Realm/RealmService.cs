using MyProject.Models.Realm;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class RealmService : IRealmService
    {
        private static Realm _realmDb;
        private static UserDataObject userDataObject;
        public RealmService()
        {
            _realmDb = Realm.GetInstance();
        }
        public async Task<string> AddUserDataAsync(string id,string userName,string userUsn,string userEmail,string userPassword, string semester, string section, string course, bool isAdmin, CancellationToken ct)
        {
            userDataObject = new UserDataObject()
            {
                UserId = id,
                UserName = userName,
                UserUsn = userUsn,
                UserEmail = userEmail,
                UserPassword = userPassword,
                Semester = semester,
                Section = section,
                Course = course,
                IsAdmin = isAdmin,
            };

           await _realmDb.WriteAsync(() =>{
                _realmDb.Add(userDataObject);
            });

            return userDataObject.UserId;
        }

        public Task<string> DeleteUserDataAsync(UserDataObject userDataObject, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
