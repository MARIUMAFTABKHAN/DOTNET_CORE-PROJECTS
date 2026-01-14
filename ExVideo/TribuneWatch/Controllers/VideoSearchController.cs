using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;        // ← Path, Directory
using System.Linq;      // ← Any, TakeWhile
using System.Text;      // ← StringBuilder
using TribuneWatch.Data;

[ApiController]
[Route("api/[controller]")]
public class VideoSearchController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<VideoSearchController> _log;
    private readonly MediaOptions _mediaOptions;
    private static readonly FileExtensionContentTypeProvider _mime = new();


    public VideoSearchController(
    AppDbContext db,
    IWebHostEnvironment env,
    ILogger<VideoSearchController> log,
    IOptions<MediaOptions> mediaOptions)
    {
        _db = db;
        _env = env;
        _log = log;
        _mediaOptions = mediaOptions.Value;

        _mime.Mappings[".dv"] = "video/x-dv";

        _log.LogInformation("WebRootPath = {WebRoot}, ContentRootPath = {ContentRoot}",
            _env.WebRootPath, _env.ContentRootPath);

        _log.LogInformation("MediaOptions: AllowedRoots = [{Roots}], DefaultRelativeFolder = {Folder}",
            string.Join(", ", _mediaOptions.AllowedRoots ?? Array.Empty<string>()),
            _mediaOptions.DefaultRelativeFolder);
    }


    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] VideoSearchRequest req, CancellationToken ct)
    {

        _db.Database.SetCommandTimeout(TimeSpan.FromSeconds(120));


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

        //var rows = await query.ToListAsync(ct);
        //return Ok(rows);

        try
        {
            var rows = await query.ToListAsync(ct);
            _log.LogInformation("✅ Search returned {Count} rows.", rows.Count);
            return Ok(rows);
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            // Client aborted the request (browser/tab closed etc.)
            _log.LogInformation("Video search cancelled by client. {@Req}", req);
            return StatusCode(499, new { error = "Client closed request" });
        }
        catch (DbUpdateException ex) when (ex.GetBaseException() is SqlException sql)
        {
            // EF wrapped a SqlException
            _log.LogError(ex, "Video search failed (SQL wrapped). {@Req}", req);
            return SqlProblem(sql);
        }
        catch (SqlException ex)
        {
            _log.LogError(ex, "Video search failed (SQL). {@Req}", req);
            return SqlProblem(ex);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Video search failed (Unhandled). {@Req}", req);
            return Problem(
                title: "Unexpected error during video search",
                detail: _env.IsDevelopment() ? ex.ToString() : ex.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }


    private ObjectResult SqlProblem(SqlException ex)
    {
        // Don’t leak full stack in production
        var detail = _env.IsDevelopment()
            ? ex.ToString()
            : $"{ex.Message} (Number {ex.Number}, Procedure {ex.Procedure}, Line {ex.LineNumber})";

        return Problem(
            title: "Database error while executing uspT_VideoSearching",
            detail: detail,
            statusCode: StatusCodes.Status500InternalServerError);
    }



    // /api/videos/{archiveId}/download
    [AllowAnonymous]
    [HttpGet("{archiveId:long}/download")]
    public async Task<IActionResult> Download(long archiveId, CancellationToken ct)
    {
        _db.Database.SetCommandTimeout(60);

        var rows = await _db.VideoSearchRows
            .FromSqlInterpolated($@"EXEC dbo.uspT_VideoSearching @p_ArchiveID = {archiveId}")
            .AsNoTracking()
            .ToListAsync(ct);

        var item = rows.FirstOrDefault();
        if (item is null)
            return NotFound(new { message = "Archive not found", archiveId });

        if (string.IsNullOrWhiteSpace(item.FileName))
            return NotFound(new { message = "No filename for this record", archiveId });

        _log.LogInformation("Attempting to resolve file for ArchiveID={ArchiveID}, FileName={FileName}",
            archiveId, item.FileName);

        var normalized = NormalizeNetworkPath(item.FileName);

        // ✅ Direct check first
        if (!System.IO.File.Exists(normalized))
        {
            _log.LogWarning("Direct file not found at {Path}. Initiating recursive search...", normalized);
            normalized = FindInNewsFolders(Path.GetFileName(normalized));

            if (normalized == null)
            {
                _log.LogWarning("File not found in any NEWS subfolder for {ArchiveID}.", archiveId);
                return NotFound(new
                {
                    message = "File not found on network path or in any NEWS subfolder",
                    fileName = item.FileName
                });
            }
        }

        _log.LogInformation("Resolved path: {Path}", normalized);

        if (!_mime.TryGetContentType(normalized, out var contentType))
        {
            var ext = Path.GetExtension(normalized).ToLowerInvariant();
            contentType = ext switch
            {
                ".wmv" => "video/x-ms-wmv",
                ".mp4" => "video/mp4",
                ".mov" => "video/quicktime",
                ".avi" => "video/x-msvideo",
                _ => "application/octet-stream"
            };
        }

        try
        {
            return PhysicalFile(normalized, contentType, enableRangeProcessing: true);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error serving file {Path}", normalized);
            return Problem(
                title: "Error serving video file",
                detail: _env.IsDevelopment() ? ex.ToString() : ex.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }



    // ✅ Convert mapped drive (Z:, M:) to UNC and add default NEWS root
    private string NormalizeNetworkPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return path ?? string.Empty;

        path = path.Trim();

        // Handle mapped drives
        path = path.Replace("Z:\\", @"\\172.18.11.9\vfs\")
                   .Replace("M:\\", @"\\172.18.11.9\vfs\")
                   .Replace(@"\\?\Z:\", @"\\172.18.11.9\vfs\")
                   .Replace(@"\\?\M:\", @"\\172.18.11.9\vfs\");

        // If only filename given, assume default NEWS folder
        if (!path.Contains('\\') && !path.Contains('/'))
        {
            path = Path.Combine(@"\\172.18.11.9\vfs\NEWS", path);
        }

        // Clean up double backslashes
        while (path.Contains(@"\\\\")) path = path.Replace(@"\\\\", @"\\");
        return path;
    }


    // ✅ Search all NEWS subfolders recursively
    private string? FindInNewsFolders(string fileName)
    {
        try
        {
            string root = @"\\172.18.11.9\vfs\NEWS";
            if (!Directory.Exists(root)) return null;

            string target = CollapseSpaces(Path.GetFileNameWithoutExtension(fileName)).ToLowerInvariant();

            foreach (var file in Directory.EnumerateFiles(root, "*.avi", SearchOption.AllDirectories))
            {
                string candidate = CollapseSpaces(Path.GetFileNameWithoutExtension(file)).ToLowerInvariant();

                // If filename almost matches (tolerate typos or minor changes)
                if (candidate.Contains(target) || target.Contains(candidate))
                {
                    _log.LogInformation("Fuzzy matched file: {Match}", file);
                    return file;
                }
            }
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error during fuzzy file search: {FileName}", fileName);
        }

        return null;
    }
    // ===== Helpers =====

    private static string CollapseSpaces(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return s ?? string.Empty;
        var arr = s.Trim().ToCharArray();
        var sb = new StringBuilder(arr.Length);
        bool prevSpace = false;
        foreach (var ch in arr)
        {
            bool isSpace = char.IsWhiteSpace(ch);
            if (isSpace)
            {
                if (!prevSpace) sb.Append(' ');
            }
            else
            {
                sb.Append(ch);
            }
            prevSpace = isSpace;
        }
        return sb.ToString();
    }

    // Canonical StartsWith for Windows paths
    private static bool PathStartsWith(string path, string root)
    {
        var p = Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var r = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        return p.StartsWith(r, StringComparison.OrdinalIgnoreCase);
    }

    
}
