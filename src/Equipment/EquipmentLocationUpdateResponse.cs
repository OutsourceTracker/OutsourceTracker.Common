using OutsourceTracker.Equipment.Trailers;

namespace OutsourceTracker.Equipment;

public class EquipmentLocationUpdateResponse<TID> where TID : struct
{
    public bool Success { get; set; }

    public TID[] SuccessfulTrailers { get; set; } = [];

    public Dictionary<TID, string> FailedTrailers { get; set; } = [];

    /// <summary>
    /// Full updated trailer models for the successful spots (populated by backend after
    /// auto zone lookup etc). Allows frontend to patch local state (including ZoneId/ZoneName)
    /// immediately without a full list reload.
    /// </summary>
    public TrailerModel[] UpdatedTrailers { get; set; } = [];
}
