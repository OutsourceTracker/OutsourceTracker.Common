namespace OutsourceTracker.Equipment;

public class EquipmentLocationUpdateResponse<TID> where TID : struct
{
    public bool Success { get; set; }

    public TID[] SuccessfulTrailers { get; set; } = [];

    public Dictionary<TID, string> FailedTrailers { get; set; } = [];
}
