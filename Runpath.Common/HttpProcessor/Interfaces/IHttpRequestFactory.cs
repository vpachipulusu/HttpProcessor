using System.Net.Http;
using System.Threading.Tasks;

namespace Runpath.Common.HttpProcessor.Interfaces
{
    public interface IHttpRequestFactory
    {
        Task<HttpResponseMessage> Get(string requestUri);
    }
}
