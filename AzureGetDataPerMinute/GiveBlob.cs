using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Atea.Services;

namespace AzureGetDataPerMinute
{
    public class GiveBlob
    {
        private readonly IBlobService _blobService;
        public GiveBlob(IBlobService blobService)
        {
            _blobService = blobService;
        }
        [FunctionName("GiveBlob")]
        public async Task<IActionResult> Run(
            [HttpTrigger("get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var id = req.Query["id"];

            var result = await _blobService.GetBlobAsync(id);

            return new OkObjectResult(result);
        }
    }
}
