using OutsourceTracker.Converters;
using OutsourceTracker.Services.ModelService;

namespace OutsourceTracker.Geolocation;

/// <summary>
/// Defines a geographic or logical zone that can contain points (e.g. trailers, tractors, drop-off locations).
/// Zones are identified by a generic struct ID and defined by a polygonal boundary.
/// </summary>
/// <typeparam name="TID">The type of the zone identifier (e.g. <see cref="int"/>, <see cref="Guid"/>, <see cref="long"/>).</typeparam>
public interface IZone<TID> : IServiceModel<TID> where TID : struct
{
    /// <summary>
    /// Gets the human-readable code of the zone (e.g. "SU2WA", "WIYA02", "SURWA").
    /// </summary>
    /// <remarks>
    /// Used for display in UI tables, dropdowns, and map labels.
    /// Should be unique within the context of the application or tenant.
    /// </remarks>
    string ShortCode { get; }

    /// <summary>
    /// Gets the human-readable name of the zone (e.g. "J.B. Hunt | Tractor Shop", "WinCo Foods DC - Yakima, WA", "J.B. Hunt | Trailer Shop")
    /// </summary>
    string FullName { get; }

    /// <summary>
    /// Gets the collection of entry points represented as two-dimensional vectors.
    /// </summary>
    ICollection<Vector2> EntryPoints { get; }

    /// <summary>
    /// Gets the collection of exit points represented as two-dimensional vectors.
    /// </summary>
    /// <remarks>The collection contains the coordinates of all defined exit points. The order of the points
    /// in the collection is not guaranteed.</remarks>
    ICollection<Vector2> ExitPoints { get; }

    /// <summary>
    /// Gets the collection of dock points represented as two-dimensional vectors.
    /// </summary>
    /// <remarks>The collection contains the coordinates of all defined dock points. The order of the points
    /// in the collection is not guaranteed.</remarks>
    ICollection<Vector2> DockPoints { get; }

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
