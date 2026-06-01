using System.Collections.Generic;

namespace OutsourceTracker.Equipment.Trailers;

/// <summary>
/// Request payload for bulk updating multiple trailers with the same set of changes.
/// </summary>
public class BulkTrailerUpdateRequest
{
    /// <summary>
    /// The IDs of the trailers to update.
    /// </summary>
    public Guid[] Ids { get; set; } = [];

    /// <summary>
    /// The changes to apply to all specified trailers (patch-style).
    /// Example: { "State": "InMaintenance", "AccountId": "..." }
    /// </summary>
    public IDictionary<string, object> Changes { get; set; } = new Dictionary<string, object>();
}
