namespace OutsourceTracker.Services.DataModels;

/// <summary>
/// Represents the result of a bulk create operation, allowing partial success.
/// </summary>
/// <typeparam name="T">The type of successfully created models.</typeparam>
public class BulkCreateResult<T>
{
    /// <summary>
    /// Successfully created items.
    /// </summary>
    public List<T> Created { get; set; } = [];

    /// <summary>
    /// Items that failed to create. Key is a human-readable identifier for the item
    /// (e.g. "ABC 001234" for trailers), value is the error message.
    /// </summary>
    public Dictionary<string, string> Failed { get; set; } = [];
}
