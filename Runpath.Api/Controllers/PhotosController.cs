using Microsoft.AspNetCore.Mvc;
using Runpath.Services.V1.Interfaces;
using System;
using System.Threading.Tasks;

namespace Runpath.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoService"></param>
        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Get photos based on user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                return Ok(await _photoService.GetUserSpecificAlbumPhotos(userId));
            }
            catch (Exception)
            {
                return BadRequest();
            }          
        }
    }
}
