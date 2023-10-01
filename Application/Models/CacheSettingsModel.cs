namespace Application.Models
{
    public class CacheSettingsModel
    {
        public int SlidingExpiration { get; set; } // There is sliding expiration and absolute expiration.
        public string? DestinationUrl { get; set; }
        public string? ApplicationName { get; set;}
        public bool BypassCache { get; set; }
    }
}
