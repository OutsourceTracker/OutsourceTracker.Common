namespace OutsourceTracker.Services.DataModels;

/// <summary>
/// Result of a bulk update operation (same changes applied to multiple records).
/// </summary>
/// <typeparam name="TModel">The model type returned for successfully updated records.</typeparam>
public class BulkUpdateResult<TModel>
{
    /// <summary>
    /// Successfully updated models (after changes applied).
    /// </summary>
    public List<TModel> Updated { get; set; } = [];

    /// <summary>
    /// IDs that failed to update, with error message.
    /// </summary>
    public Dictionary<Guid, string> Failed { get; set; } = [];
}
