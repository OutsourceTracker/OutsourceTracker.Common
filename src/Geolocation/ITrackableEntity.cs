using OutsourceTracker.Geolocation.Converters;
using System.Text.Json.Serialization;

namespace OutsourceTracker.Geolocation;

public interface ITrackableEntity<TID> where TID : struct
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
    [JsonConverter(typeof(NullableVector2JsonConverter))]
    Vector2? Location { get; set; }

    /// <summary>
    /// Gets the optional accuracy of the reported location in meters, indicating the radius of uncertainty around the location point.
    /// </summary>
    double? LocationAccuracy { get; set; }

    /// <summary>
    /// Gets the optional identifier of the zone or area (e.g., a geofence or location group) where the equipment was last reported.
    /// </summary>
    TID? ZoneId { get; set; }

    /// <summary>
    /// Gets the optional name of the zone or area (e.g., a geofence or location group) where the equipment was last reported.
    /// </summary>
    string? ZoneName { get; set; }
}
