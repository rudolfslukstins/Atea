using System;

namespace Atea
{
    public interface IAzureConfiguration
    {
        string ConnectionString => Environment.GetEnvironmentVariable("UseDevelopmentStorage=true");
        string BlobContainerName => Environment.GetEnvironmentVariable("atea-api-blob");
        string AzureTableName => Environment.GetEnvironmentVariable("data");
        string FilePrefix => Environment.GetEnvironmentVariable("ApiResponse-");
    }
}