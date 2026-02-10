namespace OutsourceTracker.Geolocation;

/// <summary>
/// Represents a geographic coordinate point with latitude, longitude, and accuracy.
/// This struct is used in the OutsourceTracker application for storing and managing location data,
/// particularly for equipment tracking, enabling precise geolocation features such as mapping and proximity calculations.
/// </summary>
public struct MapCoordinates
{
    /// <summary>
    /// Gets the latitude component of the coordinate in degrees.
    /// </summary>
    public double Latitude { get; }

    /// <summary>
    /// Gets the longitude component of the coordinate in degrees.
    /// </summary>
    public double Longitude { get; }

    /// <summary>
    /// Gets the accuracy of the coordinate measurement, typically in meters.
    /// </summary>
    public double Accuracy { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MapCoordinates"/> struct with the specified latitude, longitude, and accuracy.
    /// </summary>
    /// <param name="lat">The latitude in degrees.</param>
    /// <param name="lng">The longitude in degrees.</param>
    /// <param name="acc">The accuracy in meters.</param>
    public MapCoordinates(double lat, double lng, double acc)
    {
        Latitude = lat;
        Longitude = lng;
        Accuracy = acc;
    }

    /// <summary>
    /// Gets a static instance representing a zero or default coordinate (0.0 latitude, 0.0 longitude, 0.0 accuracy).
    /// This can be used as a sentinel value or initial state in location tracking scenarios.
    /// </summary>
    public static MapCoordinates Zero { get; } = new MapCoordinates(0.0, 0.0, 0.0);
}