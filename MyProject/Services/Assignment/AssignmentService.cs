using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class AssignmentService : IAssignmentService
    {
        private static string BaseUrl => Constants.ApiBaseUrl;
        private static RestClient _restClient;
        public AssignmentService()
        {
            _restClient = new RestClient(BaseUrl);

        }
        public async Task<RestResponse> AddAssignmentAsync(string semester, string subjectName, string subjectImage, string assignmentQuestion, string dueDate, CancellationToken ct)
        {
            var request = new RestRequest(Constants.AddAssignments, Method.Get);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("id", Guid.NewGuid().ToString());
            request.AddParameter("semester", semester);
            request.AddParameter("subjectName", subjectName);
            request.AddParameter("subjectImage", subjectImage);
            request.AddParameter("assignmentQuestion", assignmentQuestion);
            request.AddParameter("assignedOn", DateTime.Now.ToString("dd,MM,yyyy hh:mm tt"));
            request.AddParameter("dueDate", dueDate);

            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }

        public async Task<RestResponse> AssignmentAsync(string semester,string usn,CancellationToken ct)
        {
            var request = new RestRequest(Constants.Assignments, Method.Get);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("sem", semester);
            request.AddParameter("usn", usn);
            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }

        public Task<RestResponse> DeleteAssignmentAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> FilterAssignmentAsync(string category, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> UpdateAssignmentAsync(string Id, string marks, CancellationToken ct)
        {
            var request = new RestRequest(Constants.UpdateAssignments, Method.Post);
            request.AddParameter("id", Id);
            request.AddParameter("marks", marks);
            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }

        public async Task<RestResponse> SubmitAssignmentAsync(string assignmentId, FileResult assignmentFile, string isSubmitted, CancellationToken ct)
        {
            var request = new RestRequest(Constants.SubmitAssignment, Method.Post);
            request.AddFile("UploadFile", assignmentFile.FullPath, assignmentFile.ContentType);
            request.AddParameter("id", assignmentId, ParameterType.GetOrPost);
            request.AddParameter("IsSubmitted", isSubmitted, ParameterType.GetOrPost);

            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }

        public async Task<RestResponse> AllAssignmentAsync(CancellationToken ct)
        {
            var request = new RestRequest(Constants.AllAssignments, Method.Get);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }

        public async Task<RestResponse> SubmittedAssignmentAsync(string assignmentId, CancellationToken ct)
        {
            var request = new RestRequest(Constants.SubmittedAssignment, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("assignmentId",assignmentId);
            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }
    }
}
