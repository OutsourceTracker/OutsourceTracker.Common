namespace OutsourceTracker.Geolocation;

/// <summary>
/// A interface for spotting and updating location
/// </summary>
/// <typeparam name="TID">The models Id property type</typeparam>
/// <typeparam name="TModel">The model this interface represents</typeparam>
/// <typeparam name="TReturn">The type of the result returned by the location update operation, which can be the updated model, a status object, or any relevant data.</typeparam>
public interface ITrackableLocationService<TID, TModel, TReturn> where TID : struct where TModel : ITrackableEntity<TID>
{
    /// <summary>
    /// Updates the current location of an equipment record.
    /// </summary>
    /// <param name="id">Primary key of the equipment (e.g. Guid)</param>
    /// <param name="coordinates">New latitude/longitude (optional timestamp)</param>
    /// <param name="accuracy">Optional accuracy of the location data in meters</param>
    /// <param name="cancellationToken">For long-running or user-cancelled calls</param>
    Task<TReturn> UpdateLocation(TID id, Vector2 coordinates, double? accuracy = null, CancellationToken cancellationToken = default);
}

/// <summary>
/// A interface for spotting and updating location
/// </summary>
/// <typeparam name="TModel">The model this interface represents</typeparam>
/// <typeparam name="TReturn">The type of the result returned by the location update operation, which can be the updated model, a status object, or any relevant data.</typeparam>
public interface ITrackableLocationService<TModel, TReturn> : ITrackableLocationService<Guid, TModel, TReturn> where TModel : ITrackableEntity<Guid> { }
