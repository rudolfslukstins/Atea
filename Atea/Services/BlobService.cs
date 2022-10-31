using Atea.Models;
using System.Threading.Tasks;

namespace Atea.Services
{
    public class BlobService : IBlobService
    {
        private readonly IStorageProvider _storageProvider;

        public BlobService(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public string SaveBlob(Root response)
        {
            return _storageProvider.SaveBlob(response);
        }

        public Task<string> GetBlobAsync(string id)
        {
            return _storageProvider.GetBlobAsync(id);
        }
    }
}