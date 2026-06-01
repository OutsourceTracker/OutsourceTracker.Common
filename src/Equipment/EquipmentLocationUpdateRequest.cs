using OutsourceTracker.Geolocation;

namespace OutsourceTracker.Equipment;

public class EquipmentLocationUpdateRequest<TID> where TID : struct
{
    public TID[] Ids { get; set; } = [];

    public Vector2 Location { get; set; } = new Vector2();

    public double Accuracy { get; set; } = 0.0;
}
