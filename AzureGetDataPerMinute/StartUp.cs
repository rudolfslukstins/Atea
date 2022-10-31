using Atea.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using Atea;
using Atea.Azure;

[assembly: FunctionsStartup(typeof(AzureGetDataPerMinute.StartUp))]

namespace AzureGetDataPerMinute;

public class StartUp : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .AddRefitClient<IPublicApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.publicapis.org"));
        builder.Services.AddScoped<StorageProvider>();
        builder.Services.AddSingleton<IAzureConfiguration ,AzureConfiguration>();
        builder.Services.AddScoped<IBlobService, BlobService>();
        builder.Services.AddScoped<ILogService, LogService>();
    }
}