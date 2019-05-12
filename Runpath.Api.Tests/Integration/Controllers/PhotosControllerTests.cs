
using Runpath.Common.Helpers;
using Runpath.Common.HttpProcessor;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Runpath.Api.Tests.Integration.Controllers
{
    public class PhotosControllerTests
    {
        private readonly string _photosUri = $"{AppSettingsConfiguration.AppSetting("GeneralAppSettings:RunpathPhotosApiUri")}";


        [Fact]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            HttpRequestFactory httpRequestFactory = new HttpRequestFactory();
            var response = await httpRequestFactory.Get($"{_photosUri}/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
