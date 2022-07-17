using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private static string BaseUrl => Constants.ApiBaseUrl;
        private static RestClient _restClient;
        public AnnouncementService()
        {
            _restClient = new RestClient(BaseUrl);
        }
        public async Task<RestResponse> AddAnnouncementAsync(string id, string title, string description, string category , string dateTime,CancellationToken ct)
        {
            var request = new RestRequest(Constants.AddAnnouncement, Method.Post);
            request.AddParameter("id", id);
            request.AddParameter("title", title);
            request.AddParameter("descrption", description);
            request.AddParameter("category", category);
            request.AddParameter("dateTime", dateTime);
            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }

        public async Task<RestResponse> AnnouncementAsync(CancellationToken ct)
        {
            var request = new RestRequest(Constants.Announcement, Method.Get);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }

        public Task<RestResponse> DeleteAnnouncementAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> UpdateAnnouncementAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> FilterAnnouncementAsync(string category, CancellationToken ct)
        {
            var request = new RestRequest(Constants.FilterAnnouncement, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"Category={category}", ParameterType.RequestBody);
            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }
    }
}
