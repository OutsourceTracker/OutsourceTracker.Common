namespace OutsourceTracker.ModelService.Requests.Trailers;

public class TrailerFindRequest : FindRequest
{
    public string? Prefix { get; set; }

    public string? Name { get; set; }

    public string? SpottedBy { get; set; }
}
