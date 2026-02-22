namespace OutsourceTracker.Geolocation;

/// <summary>
/// A interface for spotting and updating location
/// </summary>
/// <typeparam name="TID">The models Id property type</typeparam>
/// <typeparam name="TModel">The model this interface represents</typeparam>
public interface ITrackableLocationService<TID, TModel> where TID : struct where TModel : ITrackableEntity<TID>
{
    /// <summary>
    /// Updates the current location of an equipment record.
    /// </summary>
    /// <param name="id">Primary key of the equipment (e.g. Guid)</param>
    /// <param name="coordinates">New latitude/longitude (optional timestamp)</param>
    /// <param name="cancellationToken">For long-running or user-cancelled calls</param>
    Task UpdateLocation(TID id, Vector2 coordinates, CancellationToken cancellationToken = default);
}

/// <summary>
/// A interface for spotting and updating location
/// </summary>
/// <typeparam name="TModel">The model this interface represents</typeparam>
public interface ITrackableLocationService<TModel> : ITrackableLocationService<Guid, TModel> where TModel : ITrackableEntity<Guid> { }
