using MyProject.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class UserService : IUserService
    {
        private static string BaseUrl => Constants.ApiBaseUrl;
        private static RestClient _restClient;

        public UserService()
        {
            _restClient = new RestClient(BaseUrl);
        }
        

        public Task<Guid> DeleteUserAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateUserAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> AddUserAsync(Guid id, string usn, string firstName, string lastName, string emailAddress, string password, int semester, char section, DateTime dob, string address, string mobileNumber, string parentMobile, string course, string gender, CancellationToken ct)
        {
            var request = new RestRequest(Constants.UserRegister, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"id={id}&usn={usn}&first_name={firstName}&last_name={lastName}&email_address={emailAddress}&password={password}&semester={semester}&section={section}&dob={dob.ToString("yyyy/MM/dd")}&&address={address}&mobile_number={mobileNumber}&parents_mobile={parentMobile}&course={course}&gender={gender}",
                ParameterType.RequestBody);
            var result = await _restClient.ExecutePostAsync(request);

            return result;
        }

        public async Task<RestResponse> UserLoginAsync(string emailAddress, string userPassword, CancellationToken ct)
        {
            var request = new RestRequest(Constants.Userlogin, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded",$"email_address={emailAddress}&password={userPassword}",ParameterType.RequestBody);

            var result = await _restClient.ExecutePostAsync(request);

            return result;
        }

        public async Task<RestResponse> UserByIdAsync(string id, CancellationToken ct)
        {
            var request = new RestRequest(Constants.UserById, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"id={id}", ParameterType.RequestBody);

            var result = await _restClient.ExecutePostAsync(request);

            return result;
        }
    }
}