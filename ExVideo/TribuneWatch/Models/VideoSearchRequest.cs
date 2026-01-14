public sealed class VideoSearchRequest
{
    public long? ArchiveID { get; set; }

    public int? ClassificationID { get; set; }
    public string? JTSSlug { get; set; }
    public string? FootageDetail { get; set; }
    public int? SourceID { get; set; }
    public string? Keyword { get; set; }
    public int? JTSBureau { get; set; }
    public int? PhotographerId { get; set; }
    public DateTime? ShootDateFrom { get; set; }
    public DateTime? ShootDateTo { get; set; }
    public bool? EverGreen { get; set; }
    public bool? ExactWordSearch { get; set; }
    public int? FootageTypeID { get; set; } // 0→(24,26)
}
