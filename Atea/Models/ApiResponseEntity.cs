using System;
using Microsoft.Azure.Cosmos.Table;

namespace Atea.Models
{
    public class ApiResponseEntity : TableEntity , ITableEntity
    {
        public ApiResponseEntity()
        {
            
        }

        public ApiResponseEntity(string id, string serializedResponse, DateTime executedTime)
        {
            RowKey = id;
            PartitionKey = id;
            Response = serializedResponse;
            Timestamp = executedTime;
        }

        public string Response { get; set; }
    }
}