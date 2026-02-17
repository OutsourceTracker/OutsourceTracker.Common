using OutsourceTracker.Converters;
using System.Text.Json.Serialization;

namespace OutsourceTracker.Geolocation;

public interface ITrackableEntity
{
    /// <summary>
    /// Gets the optional identifier (usually a username, account short name, or external reference)
    /// of the person or system that last reported the equipment's location.
    /// </summary>
    string? LocatedBy { get; set; }

    /// <summary>
    /// Gets the optional timestamp when the equipment's location was last recorded.
    /// </summary>
    DateTimeOffset? LocatedDate { get; set; }

    /// <summary>
    /// Gets the optional geographic location (latitude/longitude) where the equipment was last reported.
    /// </summary>
    [JsonConverter(typeof(NullableMapCoordinatesConverter))]
    MapCoordinates? Location { get; set; }
}
