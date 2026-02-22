namespace OutsourceTracker.Geolocation;

/// <summary>
/// Defines a geographic or logical zone that can contain points (e.g. trailers, tractors, drop-off locations).
/// Zones are identified by a generic struct ID and defined by a polygonal boundary.
/// </summary>
/// <typeparam name="TID">The type of the zone identifier (e.g. <see cref="int"/>, <see cref="Guid"/>, <see cref="long"/>).</typeparam>
public interface IZone<TID> where TID : struct
{
    /// <summary>
    /// Gets the unique identifier of the zone.
    /// </summary>
    /// <remarks>
    /// This is typically the primary key in the database.
    /// </remarks>
    TID Id { get; }

    /// <summary>
    /// Gets the human-readable name of the zone (e.g. "Main Yard", "Customer A Drop Zone", "Service Area North").
    /// </summary>
    /// <remarks>
    /// Used for display in UI tables, dropdowns, and map labels.
    /// Should be unique within the context of the application or tenant.
    /// </remarks>
    string Name { get; }

    /// <summary>
    /// Gets the polygonal boundary that defines the zone.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The polygon is represented using the custom <see cref="Polygon"/> struct, which stores a sequence
    /// of <see cref="Vector2"/> points (using <see langword="double"/> coordinates for geospatial precision).
    /// </para>
    /// <para>
    /// Coordinates are assumed to be in a projected or geographic system (typically WGS84 lat/lon).
    /// </para>
    /// </remarks>
    Polygon Boundry { get; }
}
