using Atea.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Atea;

namespace AzureGetDataPerMinute
{
    public class Function1
    {
        public IPublicApi _Api;
        private readonly StorageProvider _storageProvider;
        public Function1(IPublicApi api, StorageProvider storageProvider)
        {
            _Api = api;
            _storageProvider = storageProvider;
        }
        [FunctionName("ApiPull")]
        public async Task Run([TimerTrigger("*/10 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var result = await _Api.GetApi();
            var output = result.entries.First();

            log.LogInformation($" {DateTime.Now} API: {output.API} Description: {output.Description}");

            var id = _storageProvider.SaveBlob(result);

            await _storageProvider.LogRequest(result, id);
        }
    }

}
