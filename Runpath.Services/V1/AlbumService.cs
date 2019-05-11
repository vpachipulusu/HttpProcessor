using Newtonsoft.Json;
using Runpath.Common.Helpers;
using Runpath.Common.HttpProcessor;
using Runpath.Dto.V1;
using Runpath.Services.V1.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Runpath.Services.V1
{
    public class AlbumService : IAlbumService
    {
        public async Task<List<AlbumViewModel>> GetUserAlbums(int userId)
        {
            string jsonServiceApiUri = $"{AppSettingsConfiguration.AppSetting("ThirdPartyApiUris:JsonHolderAlbumsUri")}?userId={userId}";
            var response = await HttpRequestFactory.Get(jsonServiceApiUri);
            var responseData = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<AlbumViewModel>>(responseData);
        }
    }
}
