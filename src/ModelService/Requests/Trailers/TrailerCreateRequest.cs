namespace OutsourceTracker.ModelService.Requests.Trailers;

public class TrailerCreateRequest
{
    /// <summary>
    /// Get or set the prefix used to help identify trailers organizational unit
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// Get or set the trailer number associated with the trailer
    /// </summary>
    public string? Name { get; set; }
}
