using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WillisTowersWatson.Dto.ViewModels;
using WillisTowersWatson.Services.Services.Interfaces;

namespace WillisTowersWatson.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly ICumulativePoliciesService _cumulativePoliciesService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cumulativePoliciesService"></param>
        public FileUploadController(ICumulativePoliciesService cumulativePoliciesService)
        {
            this._cumulativePoliciesService = cumulativePoliciesService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            List<NonCumulativePoliciesViewModel> nonCumulativePoliciesViewModels = _cumulativePoliciesService.NonCumulativeInputFileReader(path);
            string downloadFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "OutputFile.csv");
            _cumulativePoliciesService.CumulativeOutputFile(nonCumulativePoliciesViewModels, ref downloadFile);

            var memory = new MemoryStream();
            using (var stream = new FileStream(downloadFile, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(downloadFile), Path.GetFileName(downloadFile));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".csv", "text/csv"}
            };
        }
    }
}