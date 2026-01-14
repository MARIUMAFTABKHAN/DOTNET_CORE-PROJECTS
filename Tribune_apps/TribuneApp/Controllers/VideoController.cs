using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using TribuneApp.Models;

namespace TribuneApp.Controllers
{
    [Route("api/videos")]
    [ApiController]
    public class VideoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public VideoController(IHttpClientFactory factory, IConfiguration config)
        {
            _httpClientFactory = factory;
            _config = config;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] VideoSearchRequest req)
        {
            var client = _httpClientFactory.CreateClient();
            var query = new Dictionary<string, string?>
            {
                ["ArchiveID"] = req.ArchiveID,
                ["JTSSlug"] = req.JTSSlug,
                ["Keyword"] = req.Keyword,
                ["ShootDateFrom"] = req.ShootDateFrom,
                ["ShootDateTo"] = req.ShootDateTo,
                ["ExactWordSearch"] = req.ExactWordSearch?.ToString(),
                ["FootageTypeID"] = req.FootageTypeID
            };

            var url = QueryHelpers.AddQueryString("http://61.5.152.140:90/api/VideoSearch/Search", query);

            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }

        [HttpGet("{archiveId}/download")]
        public IActionResult Download(string archiveId)
        {
            // Build file path and return File result
            var filePath = $"\\\\172.18.11.9\\vfs\\NEWS\\{archiveId}.mp4";
            var mimeType = "video/mp4";
            return PhysicalFile(filePath, mimeType, enableRangeProcessing: true);
        }
    }
}
