using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Runpath.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMediaItem(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                file.FileName);

            //var stream = file.OpenReadStream();
            //var name = Path.GetFileName(file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var downloadStream = new MemoryStream(System.IO.File.ReadAllBytes(path));

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(downloadStream)
            };

            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = "test.csv"
            };

            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");

            return Ok(result);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="filename"></param>
        //[HttpPost]
        //public async Task<IActionResult> DownloadFileAsync(string filePath)
        //{

        //    //if (filename == null)
        //    //    return Content("filename not present");

        //    //var path = Path.Combine(
        //    //    Directory.GetCurrentDirectory(),
        //    //    "wwwroot", filename);

        //    //var memory = new MemoryStream();
        //    //using (var stream = new FileStream(path, FileMode.Open))
        //    //{
        //    //    await stream.CopyToAsync(memory);
        //    //}
        //    //memory.Position = 0;
        //    //return File(memory, GetContentType(path), Path.GetFileName(path));

        //    try
        //    {
        //        var stream = new MemoryStream(System.IO.File.ReadAllBytes(filePath));

        //        var result = new HttpResponseMessage(HttpStatusCode.OK)
        //        {
        //            Content = new StreamContent(stream)
        //        };

        //        result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        //        {
        //            FileName = "test.csv"
        //        };

        //        result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

        //        return Ok(result);
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        return BadRequest();
        //    }

        //}

    }
}