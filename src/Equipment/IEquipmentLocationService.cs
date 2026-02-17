using OutsourceTracker.Geolocation;

namespace OutsourceTracker.Equipment;

/// <summary>
/// A interface for spotting and updating equipment location
/// </summary>
/// <typeparam name="TID">The models Id property type</typeparam>
/// <typeparam name="TModel">The model this interface represents</typeparam>
public interface IEquipmentLocationService<TID, TModel> where TID : struct where TModel : IEquipment<TID>
{
    /// <summary>
    /// Updates the current location of an equipment record.
    /// </summary>
    /// <param name="id">Primary key of the equipment (e.g. Guid)</param>
    /// <param name="mapCoordinates">New latitude/longitude (optional timestamp)</param>
    /// <param name="cancellationToken">For long-running or user-cancelled calls</param>
    Task UpdateLocation(TID id, MapCoordinates mapCoordinates, CancellationToken cancellationToken = default);
}

/// <summary>
/// A interface for spotting and updating equipment location
/// </summary>
/// <typeparam name="TModel">The model this interface represents</typeparam>
public interface IEquipmentLocationService<TModel> : IEquipmentLocationService<Guid, TModel> where TModel : IEquipment<Guid> { }
