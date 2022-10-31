using Refit;
using System.Threading.Tasks;
using Atea.Models;

namespace Atea.Services
{
    public interface IPublicApi
    {
        [Get("/random?auth=null")]
        Task<Root> GetApi();
    }
}