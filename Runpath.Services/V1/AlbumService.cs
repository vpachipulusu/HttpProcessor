using Newtonsoft.Json;
using Runpath.Common.Helpers;
using Runpath.Common.HttpProcessor.Interfaces;
using Runpath.Dto.V1;
using Runpath.Services.V1.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Runpath.Services.V1
{
    public class AlbumService : IAlbumService
    {
        private readonly IHttpRequestFactory _httpRequestFactory;

        public AlbumService(IHttpRequestFactory httpRequestFactory)
        {
            _httpRequestFactory = httpRequestFactory;
        }

        public async Task<List<AlbumViewModel>> GetUserAlbums(int userId)
        {
            string jsonServiceApiUri = $"{AppSettingsConfiguration.AppSetting("ThirdPartyApiUris:JsonHolderAlbumsUri")}";
            var response = await _httpRequestFactory.Get(jsonServiceApiUri);
            var responseData = response.Content.ReadAsStringAsync().Result;
            List<AlbumViewModel> albumViewModels = JsonConvert.DeserializeObject<List<AlbumViewModel>>(responseData);
            List<AlbumViewModel> filteredAlbumViewModels = albumViewModels.Where(a => a.UserId == userId).ToList();
            return filteredAlbumViewModels;
        }
    }
}
