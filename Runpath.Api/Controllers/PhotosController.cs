using Microsoft.AspNetCore.Mvc;
using Runpath.Services.V1.Interfaces;
using System.Threading.Tasks;

namespace Runpath.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            return Ok(await _photoService.GetUserSpecificAlbumPhotos(userId));
        }
    }
}
