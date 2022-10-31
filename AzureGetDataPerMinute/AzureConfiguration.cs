using System;

namespace Atea.Azure;

public class AzureConfiguration : IAzureConfiguration
{
    public string ConnectionString => Environment.GetEnvironmentVariable("UseDevelopmentStorage=true");
    public string BlobContainerName => Environment.GetEnvironmentVariable("atea-api-blob");
    public string AzureTableName => Environment.GetEnvironmentVariable("data");
    public string FilePrefix => Environment.GetEnvironmentVariable("ApiResponse-");
}