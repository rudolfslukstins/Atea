using System.Threading.Tasks;
using Atea.Models;

namespace Atea.Services
{
    public interface IBlobService
    {
        string SaveBlob(Root response);
        Task<string> GetBlobAsync(string id);
    }
}