using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface INotesService
    {
        Task<RestResponse> NotesCategoryAsync(CancellationToken ct);
        Task<RestResponse> AddNotesAsync(string categoryId,string subjectName,string notesTitle,string semester,FileResult filePath,CancellationToken ct);
        Task<RestResponse> NotesListyAsync(string categoryId, CancellationToken ct);

        Task<Guid> AddNotesCategoryAsync(Guid Id, CancellationToken ct);
        Task<Guid> DeleteNotesAsync(Guid id, CancellationToken ct);
    }
}
