using Microsoft.AspNetCore.Mvc;
using Runpath.Services.V1.Interfaces;
using System;
using System.Threading.Tasks;

namespace Runpath.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            this._albumService = albumService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                return Ok(await _albumService.GetUserAlbums(userId));
            }
            catch (Exception)
            {
                return BadRequest();
            }          
        }
    }
}
