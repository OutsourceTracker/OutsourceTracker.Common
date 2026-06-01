namespace OutsourceTracker.Services.DataModels;

/// <summary>
/// Result of a bulk delete operation.
/// </summary>
public class BulkDeleteResult
{
    /// <summary>
    /// IDs that were successfully deleted.
    /// </summary>
    public Guid[] SuccessfulIds { get; set; } = [];

    /// <summary>
    /// IDs that failed to delete, with error message.
    /// </summary>
    public Dictionary<Guid, string> Failed { get; set; } = [];
}
