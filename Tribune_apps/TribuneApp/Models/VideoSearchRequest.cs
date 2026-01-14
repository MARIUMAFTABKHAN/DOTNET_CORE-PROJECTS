namespace TribuneApp.Models
{
    public class VideoSearchRequest
    {
        public string? ArchiveID { get; set; }
        public string? JTSSlug { get; set; }
        public string? Keyword { get; set; }
        public string? ShootDateFrom { get; set; }
        public string? ShootDateTo { get; set; }
        public bool? ExactWordSearch { get; set; }
        public string? FootageTypeID { get; set; }
    }
}
