using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IAssignmentService
    {
        Task<RestResponse> AssignmentAsync(string semester,string usn,CancellationToken ct);
        Task<RestResponse> AllAssignmentAsync(CancellationToken ct);
        Task<RestResponse> SubmitAssignmentAsync(string assignmentId, FileResult assignmentFile, string isSubmitted, CancellationToken ct);
        Task<RestResponse> SubmittedAssignmentAsync(string assignmentId, CancellationToken ct);
        Task<RestResponse> FilterAssignmentAsync(string category, CancellationToken ct);
        Task<RestResponse> AddAssignmentAsync(string semester,string subjectName,string subjectImage,string assignmentQuestion,string dueDate, CancellationToken ct);
        Task<RestResponse> UpdateAssignmentAsync(string Id,string marks, CancellationToken ct);
        Task<RestResponse> DeleteAssignmentAsync(Guid Id, CancellationToken ct);
    }
}
