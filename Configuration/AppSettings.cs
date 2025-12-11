namespace lab09webAPp.Configuration;

public class AppSettings
{
    public string ApplicationName { get; set; } = string.Empty;
    public int MaxItemsPerPage { get; set; }
    public bool EnableCaching { get; set; }
    public string Version { get; set; } = string.Empty;
}
