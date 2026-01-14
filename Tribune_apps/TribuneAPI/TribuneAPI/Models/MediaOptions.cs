namespace TribuneAPI.Models
{
    public class MediaOptions
    {
        public string[] AllowedRoots { get; set; } = Array.Empty<string>();
        public string? DefaultRelativeFolder { get; set; } = null;
        public string? DefaultContentType { get; set; } = "application/octet-stream";

        // If true, any absolute path from DB is accepted (use with caution).
        public bool AllowAnyAbsolute { get; set; } = false;
    }
}
