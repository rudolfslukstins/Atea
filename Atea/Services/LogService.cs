using Atea.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Atea.Services
{
    public class LogService : ILogService
    {
        private readonly IStorageProvider _storageProvider;

        public LogService(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public Task LogRequest(Root response, string id)
        {
            return _storageProvider.LogRequest(response, id);
        }

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to)
        {
            return _storageProvider.GetLogs(from, to);
        }
    }
}