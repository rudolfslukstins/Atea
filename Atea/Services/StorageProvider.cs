using Azure.Storage.Blobs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Atea.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.Cosmos.Table;
using CloudStorageAccount = Microsoft.Azure.Cosmos.Table.CloudStorageAccount;
using CloudTable = Microsoft.Azure.Cosmos.Table.CloudTable;
using TableClientConfiguration = Microsoft.Azure.Cosmos.Table.TableClientConfiguration;
using TableOperation = Microsoft.Azure.Cosmos.Table.TableOperation;

namespace Atea.Services
{
    public interface IBLobStorageProvider
    {
        string SaveBlob(Root response);
        Task<string> GetBlobAsync(string id);
    }

    public interface ILogStorageProvider
    {
        Task LogRequest(Root response, string id);
        IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to);
    }

    public class StorageProvider : IStorageProvider, IBLobStorageProvider, ILogStorageProvider
    {
        private readonly IAzureConfiguration _azureConfiguration;
        private readonly BlobContainerClient _blobContainerClient;
        private readonly CloudTable _cloudTable;

        public StorageProvider(IAzureConfiguration azureConfiguration)
        {
            _azureConfiguration = azureConfiguration;
            var client = new BlobServiceClient("UseDevelopmentStorage=true");

            try
            {
              _blobContainerClient = client.CreateBlobContainer(azureConfiguration.BlobContainerName);
            }
            catch (Exception e)
            {
              _blobContainerClient = client.GetBlobContainerClient(azureConfiguration.BlobContainerName);
            }

            var account = CloudStorageAccount.Parse(azureConfiguration.ConnectionString);
            var tableClient = account.CreateCloudTableClient(new TableClientConfiguration());
            _cloudTable = tableClient.GetTableReference(azureConfiguration.AzureTableName);
            _cloudTable.CreateIfNotExists();

        }

        public string SaveBlob(Root response)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);

            var content = JsonSerializer.Serialize(response);

            streamWriter.Write(content);

            streamWriter.Flush();

            stream.Seek(0, SeekOrigin.Begin);
            var name = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

            var fileName = $"{_azureConfiguration.FilePrefix}{name}.json";

            _blobContainerClient.UploadBlob(fileName, stream);
            
            return name;
        }

        public async Task LogRequest(Root response, string id)
        {
            var serialized = JsonSerializer.Serialize(response);
            var entry = new ApiResponseEntity(id, serialized, DateTime.Now);
            var operation = TableOperation.Insert(entry);
            await _cloudTable.ExecuteAsync(operation);

        }

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to)
        {
            var items = _cloudTable.ExecuteQuery(new Microsoft.Azure.Cosmos.Table.TableQuery<ApiResponseEntity>()
                ).Where(x => x.Timestamp >= from && x.Timestamp <= to);

            return items;
        }

        public async Task<string> GetBlobAsync(string id)
        {

            var fileName = $"{_azureConfiguration.FilePrefix}{id},json";

            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            using var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);

            stream.Position = 0;

            using var streamReader = new StreamReader(stream);

            var response = await streamReader.ReadToEndAsync();

            return response;
        }
    }
}