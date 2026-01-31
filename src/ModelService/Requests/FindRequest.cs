namespace OutsourceTracker.ModelService.Requests;

public class FindRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid[]? Ids { get; set; }
}
