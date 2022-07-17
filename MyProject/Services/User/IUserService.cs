using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MyProject.Services
{
    public interface IUserService
    {
        Task<RestResponse> AddUserAsync(Guid id, string usn, string firstName,string lastName,string emailAddress,
            string password,int semester,char section,DateTime dob,string address,string mobileNumber,string parentMobile,
            string course,string gender, CancellationToken ct);

        Task<RestResponse> UserLoginAsync(string emailAddress, string userPassword, CancellationToken ct);
        Task<RestResponse> UserByIdAsync(string id, CancellationToken ct);
        Task<Guid> UpdateUserAsync(Guid Id, CancellationToken ct);
        Task<Guid> DeleteUserAsync(Guid id, CancellationToken ct);

    }
}