using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text;
using TribuneAPI.Data;
using TribuneAPI.Models;
using TribuneAPI.Services;

namespace TribuneAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoSearchController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<VideoSearchController> _log;
        private readonly MediaOptions _mediaOptions;
        private static readonly FileExtensionContentTypeProvider _mime = new();
        private readonly INewsFileIndex _fileIndex;
        public VideoSearchController(INewsFileIndex fileIndex,
            AppDbContext db,
            IWebHostEnvironment env,
            ILogger<VideoSearchController> log,
            IOptions<MediaOptions> mediaOptions)
        {
            _fileIndex = fileIndex;
            _db = db;
            _env = env;
            _log = log;
            _mediaOptions = mediaOptions.Value;

            _mime.Mappings[".dv"] = "video/x-dv";
            
        }

        [HttpGet("test-access")]
        public IActionResult TestAccess()
        {
            string testPath = @"\\172.18.11.9\vfs\NEWS\Chaupal-NEWS\INT\2025\Package\October\12-10-2025";
        
            try
            {
                if (!Directory.Exists(testPath))
                {
                    return NotFound("NEWS folder not accessible.");
                }

                var files = Directory.GetFiles(testPath, "*.*", SearchOption.TopDirectoryOnly);
                return Ok(new
                {
                    message = "Folder accessed successfully.",
                    totalFiles = files.Length,
                    sample = files.Take(3).ToList()  // Return top 3 file names
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Exception accessing folder.",
                    error = ex.Message,
                    stack = ex.ToString()
                });
            }
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] VideoSearchRequest req, CancellationToken ct)
        {
            _log.LogInformation("🎯 Hit Search API with keyword: {Keyword}", req.Keyword);

            _db.Database.SetCommandTimeout(TimeSpan.FromSeconds(120));

            try
            {
                var query = _db.VideoSearchRows.FromSqlInterpolated($@"
                    EXEC dbo.uspT_VideoSearching
                        @p_nClassificationID = {req.ClassificationID},
                        @p_strJTSSlug        = {req.JTSSlug},
                        @p_strFootageDetail  = {req.FootageDetail},
                        @p_nSourceID         = {req.SourceID},
                        @p_strkeyword        = {req.Keyword},
                        @p_nJTSBureau        = {req.JTSBureau},
                        @p_nPhotographerId   = {req.PhotographerId},
                        @p_dtShootDateFrom   = {req.ShootDateFrom},
                        @p_dtShootDateTo     = {req.ShootDateTo},
                        @p_EverGreen         = {req.EverGreen},
                        @p_bexactwordsearch  = {req.ExactWordSearch},
                        @p_nFootageTypeID    = {req.FootageTypeID},
                        @p_ArchiveID         = {req.ArchiveID}
                ").AsNoTracking();

                var rows = await query.ToListAsync(ct);
                _log.LogInformation("✅ Search returned {Count} rows.", rows.Count);
                return Ok(rows);
            }
            catch (SqlException ex)
            {
                _log.LogError(ex, "SQL Error: {Message}", ex.Message);
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }


        [AllowAnonymous]
        [HttpGet("download/{archiveId:long}")]
        public async Task<IActionResult> Download(long archiveId, CancellationToken ct)
        {
            var sw = Stopwatch.StartNew();
            string finalPath = string.Empty;

            try
            {
                _db.Database.SetCommandTimeout(60);

                var rows = await _db.VideoSearchRows
                    .FromSqlInterpolated($"EXEC dbo.uspT_VideoSearching @p_ArchiveID = {archiveId}")
                    .AsNoTracking()
                    .ToListAsync(ct);

                var item = rows.FirstOrDefault();
                if (item == null || string.IsNullOrWhiteSpace(item.FileName))
                {
                    return NotFound(new
                    {
                        message = item == null ? "Archive not found" : "No filename for this record",
                        archiveId
                    });
                }

                string relativePath = item.FileName.Trim().Replace("/", "\\");
                finalPath = Path.Combine(@"\\172.18.11.9\vfs", relativePath);

                _log.LogInformation("📁 Resolved full path for download: {FinalPath}", finalPath);

                if (!System.IO.File.Exists(finalPath))
                {
                    _log.LogError("❌ File not found at resolved path: {Path}", finalPath);
                    return NotFound(new
                    {
                        message = "File not found at resolved path",
                        fileName = item.FileName,
                        attemptedPath = finalPath
                    });
                }

                // Detect content type
                if (!_mime.TryGetContentType(finalPath, out var contentType))
                {
                    var ext = Path.GetExtension(finalPath).ToLowerInvariant();
                    contentType = ext switch
                    {
                        ".wmv" => "video/x-ms-wmv",
                        ".mp4" => "video/mp4",
                        ".mov" => "video/quicktime",
                        ".avi" => "video/x-msvideo",
                        _ => "application/octet-stream"
                    };
                }

                _log.LogInformation("📦 Serving file: {Path}, ContentType: {ContentType}", finalPath, contentType);
                return PhysicalFile(finalPath, contentType, enableRangeProcessing: true);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "💥 Exception while serving file for ArchiveID={ArchiveID}", archiveId);
                return Problem(
                    title: "Error serving video file",
                    detail: _env.IsDevelopment() ? ex.ToString() : ex.Message,
                    statusCode: 500);
            }
            finally
            {
                sw.Stop();
                _log.LogInformation("⏱️ Total execution time: {Elapsed} ms", sw.ElapsedMilliseconds);
            }
        }




        [HttpGet("check-exists")]
        public IActionResult CheckExists([FromQuery] string fileName)
        {
            var full = Path.Combine(@"\\172.18.11.9\vfs", fileName.Replace("/", "\\"));

            _log.LogInformation("🔍 Checking exact file: {Path}", full);
            bool exists = System.IO.File.Exists(full);

            return Ok(new
            {
                exists,
                path = full
            });
        }

        [HttpGet("manual-scan")]
        public IActionResult ManualScan([FromQuery] string fileName)
        {
            string root = @"\\172.18.11.9\vfs";

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest(new { message = "Missing fileName parameter" });
            }

            try
            {
                if (!Directory.Exists(root))
                {
                    return NotFound(new { message = "Root folder not found or inaccessible", root });
                }

                string target = Path.GetFileName(fileName).ToLowerInvariant();

                foreach (var file in SafeEnumerateFiles(root, "*.*"))
                {
                    string current = Path.GetFileName(file).ToLowerInvariant();
                    if (current == target)
                    {
                        return Ok(new
                        {
                            message = "✅ File found",
                            fullPath = file
                        });
                    }
                }

                return NotFound(new
                {
                    message = "❌ File not found after full scan",
                    target,
                    scannedRoot = root
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Unhandled exception during scan",
                    error = ex.Message,
                    stack = ex.ToString()
                });
            }
        }

        private IEnumerable<string> SafeEnumerateFiles(string root, string pattern)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(root, pattern));
            }
            catch { }

            try
            {
                foreach (var dir in Directory.GetDirectories(root))
                {
                    files.AddRange(SafeEnumerateFiles(dir, pattern));
                }
            }
            catch { }

            return files;
        }



    }
}
