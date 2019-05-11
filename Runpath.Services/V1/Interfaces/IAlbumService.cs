using Runpath.Dto.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Runpath.Services.V1.Interfaces
{
    public interface IAlbumService
    {
        Task<List<AlbumViewModel>> GetUserAlbums(int userId);
    }
}
