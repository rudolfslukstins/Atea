using Atea.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Atea.Services
{
    public interface IStorageProvider
    {
        public Task<string> GetBlobAsync(string id);
        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to);
        public Task LogRequest(Root response, string id);
        public string SaveBlob(Root response);

    }
}