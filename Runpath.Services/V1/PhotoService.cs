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
    public class PhotoService : IPhotoService
    {
        private readonly IHttpRequestFactory _httpRequestFactory;
        private readonly IAlbumService _albumService;

        public PhotoService(IHttpRequestFactory httpRequestFactory, IAlbumService albumService)
        {
            _httpRequestFactory = httpRequestFactory;
            _albumService = albumService;
        }

        public async Task<List<PhotoViewModel>> GetUserSpecificAlbumPhotos(int userId)
        {
            string jsonServiceApiUri = $"{AppSettingsConfiguration.AppSetting("ThirdPartyApiUris:JsonHolderPhotosUri")}";
            var response = await _httpRequestFactory.Get(jsonServiceApiUri);
            var responseData = response.Content.ReadAsStringAsync().Result;
            List<PhotoViewModel> photoViewModels = JsonConvert.DeserializeObject<List<PhotoViewModel>>(responseData);
            List<AlbumViewModel> albumViewModels = await _albumService.GetUserAlbums(userId);
            var userSpecificAlbums = albumViewModels.Select(s => s.Id).ToArray();
            List<PhotoViewModel> filteredPhotoViewModels = photoViewModels.Where(p => userSpecificAlbums.Contains(p.AlbumId)).ToList();
            return filteredPhotoViewModels;
        }
    }
}
