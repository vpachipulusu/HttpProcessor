
using Runpath.Common.Helpers;
using Runpath.Common.HttpProcessor;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Runpath.Api.Tests.Controllers
{
    public class AlbumsControllerTests
    {
        private readonly string _albumsUri = $"{AppSettingsConfiguration.AppSetting("GeneralAppSettings:RunpathAlbumsApiUri")}";


        [Fact]
        public async Task GetAblums_Based_on_User_StateUnderTest()
        {
            HttpRequestFactory httpRequestFactory = new HttpRequestFactory();
            var response = await httpRequestFactory.Get($"{_albumsUri}/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
