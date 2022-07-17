using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IAnnouncementService
    {
        Task<RestResponse> AnnouncementAsync(CancellationToken ct);
        Task<RestResponse> FilterAnnouncementAsync(string category,CancellationToken ct);
        Task<RestResponse> AddAnnouncementAsync(string id,string title,string description,string category,string dateTime, CancellationToken ct);
        Task<RestResponse> UpdateAnnouncementAsync(Guid Id, CancellationToken ct);
        Task<RestResponse> DeleteAnnouncementAsync(Guid Id, CancellationToken ct);
    }
}
