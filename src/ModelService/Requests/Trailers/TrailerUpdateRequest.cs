namespace OutsourceTracker.ModelService.Requests.Trailers;

public class TrailerUpdateRequest : TrailerCreateRequest
{
    /// <summary>
    /// Gets or sets the latitude coordinate in degrees.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate in degrees.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the accuracy value associated with the result, if available.
    /// </summary>
    public double? Accuracy { get; set; }

    /// <summary>
    /// Gets or sets the name of the user who last spotted the trailer.
    /// </summary>
    public string? SpottedBy { get; set; }
}
