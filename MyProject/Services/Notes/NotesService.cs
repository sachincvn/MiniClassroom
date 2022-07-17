using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class NotesService : INotesService
    {
        private static string BaseUrl => Constants.ApiBaseUrl;
        private static RestClient _restClient;
        public NotesService()
        {
            _restClient = new RestClient(BaseUrl);
        }

        public Task<Guid> AddNotesCategoryAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteNotesAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> NotesCategoryAsync(CancellationToken ct)
        {
            var request = new RestRequest(Constants.NotesCategory, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var result = await _restClient.ExecutePostAsync(request);

            return result;
        }

        public async Task<RestResponse> NotesListyAsync(string categoryId, CancellationToken ct)
        {
            var request = new RestRequest(Constants.NotesList, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"CategoryId={categoryId}", ParameterType.RequestBody);
            var result = await _restClient.ExecutePostAsync(request);

            return result;
        }

        public async Task<RestResponse> AddNotesAsync(string categoryId, string subjectName, string notesTitle, string semester, FileResult filePath, CancellationToken ct)
        {
            var request = new RestRequest("add-notes.php", Method.Post);
            request.AddFile("UploadFile", filePath.FullPath, filePath.ContentType);
            request.AddParameter("CategoryId", categoryId, ParameterType.GetOrPost);
            request.AddParameter("SubjectName", subjectName, ParameterType.GetOrPost);
            request.AddParameter("Semester", semester, ParameterType.GetOrPost);
            request.AddParameter("NotesTitle", notesTitle, ParameterType.GetOrPost);
            var result = await _restClient.ExecutePostAsync(request);
            return result;
        }
    }
}
