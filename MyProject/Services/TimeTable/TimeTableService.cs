using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class TimeTableService : ITimeTableService
    {
        private static string BaseUrl => Constants.ApiBaseUrl;
        private static RestClient _restClient;
        public TimeTableService()
        {
            _restClient = new RestClient(BaseUrl);
        }
       

        public async Task<RestResponse> TimeTableAsync(string weekDay, string semester, string section, CancellationToken ct)
        {
            var request = new RestRequest(Constants.TimeTable, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"WeekDay={weekDay}&Semester={semester}&Section={section}", ParameterType.RequestBody);

            var result = await _restClient.ExecutePostAsync(request);

            return result;
        }

        public Task<RestResponse> UpdateTimeTableAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> TimeTableListAsync(CancellationToken ct)
        {
            var request = new RestRequest(Constants.TimeTableList, Method.Get);
            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }

        public async Task<RestResponse> AddTimeTableListAsync(string semester, string section, CancellationToken ct)
        {
            var request = new RestRequest(Constants.AddTimeTableList, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("id",Guid.NewGuid().ToString());
            request.AddParameter("semester",semester);
            request.AddParameter("section",section);
            var result = await _restClient.ExecutePostAsync(request);

            return result;
        }

        public async Task<RestResponse> AddTimeTableAsync(string timeTableId, string subject, string teacher, string startTime, string endTime, string semester, string section,string weekDay, CancellationToken ct)
        {
            var request = new RestRequest(Constants.AddTimeTable, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("id",timeTableId);
            request.AddParameter("subject",subject);
            request.AddParameter("teacher",teacher);
            request.AddParameter("startTime",startTime);
            request.AddParameter("endTime",endTime);
            request.AddParameter("semester",semester);
            request.AddParameter("section",section);
            request.AddParameter("weekDay",weekDay);

            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }
    }
}
