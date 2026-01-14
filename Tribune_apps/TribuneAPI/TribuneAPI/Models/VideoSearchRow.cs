using Microsoft.EntityFrameworkCore;

namespace TribuneAPI.Models
{

    [Keyless]
    public class VideoSearchRow
    {
        public long ArchiveID { get; set; }
        public DateTime? Shoot { get; set; }
        public string? Slug { get; set; }
        public string? Detail { get; set; }
        public string? FootageType { get; set; }
        public string? Source { get; set; }
        public string? ShootDate { get; set; }
        public string? MediaType { get; set; }
        public long? MediaNo { get; set; }
        public bool? IsIssued { get; set; }
        public bool? IsHighClip { get; set; }
        public bool? IsLowClip { get; set; }
        public string? FileName { get; set; }
        public string? Bureau { get; set; }
        public string? Photographer { get; set; }
    }
}
