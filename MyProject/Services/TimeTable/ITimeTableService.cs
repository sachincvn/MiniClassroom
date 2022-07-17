using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface ITimeTableService
    {
        Task<RestResponse> TimeTableAsync(string weekDay,string semester,string section,CancellationToken ct);
        Task<RestResponse> TimeTableListAsync(CancellationToken ct);
        Task<RestResponse> AddTimeTableListAsync(string semester,string section,CancellationToken ct);
        Task<RestResponse> AddTimeTableAsync(string timeTableId,string subject,string teacher,string startTime,string endTime,string semester,string section,string weekDay, CancellationToken ct);

        Task<RestResponse> UpdateTimeTableAsync(Guid Id, CancellationToken ct);
    }
}
