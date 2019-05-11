using Runpath.Dto.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Runpath.Services.V1.Interfaces
{
    public interface IPhotoService
    {
        Task<List<PhotoViewModel>> GetUserSpecificAlbumPhotos(int userId);
    }
}
