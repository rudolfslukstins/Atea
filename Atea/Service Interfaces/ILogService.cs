using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atea.Models;

namespace Atea.Services
{
    public interface ILogService
    {
        Task LogRequest(Root response, string id);
        IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to);
    }
}