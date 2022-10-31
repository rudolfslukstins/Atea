using Atea.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AzureGetDataPerMinute
{
    public class LogProviders
    {
        private readonly ILogService _logService;

        public LogProviders(ILogService logService)
        {
            
            _logService = logService;
        }

        [FunctionName("LogProvider")]
        public async Task<IActionResult> Run(
            [HttpTrigger("get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string from = req.Query["from"];
            string to = req.Query["to"];

            var fromDateTime = DateTime.Parse(from);
            var toDateTime = DateTime.Parse(to);

            var getLogs = _logService.GetLogs(fromDateTime, toDateTime);

            return new OkObjectResult(getLogs);
        }
    }
}
