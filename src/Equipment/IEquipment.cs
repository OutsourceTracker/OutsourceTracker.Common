using OutsourceTracker.Services;

namespace OutsourceTracker.Equipment;

public interface IEquipment<TID> : IModel<TID>
{
    string Name { get; set; }

    double? Latitude { get; set; }

    double? Longitude { get; set; }

    TID AccountId { get; set; }
}
