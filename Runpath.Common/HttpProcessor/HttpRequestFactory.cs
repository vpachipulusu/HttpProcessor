using Runpath.Common.HttpProcessor.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Runpath.Common.HttpProcessor
{
    public class HttpRequestFactory : IHttpRequestFactory
    {
        public async Task<HttpResponseMessage> Get(string requestUri)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri);

            return await builder.SendAsync();
        }
    }
}
