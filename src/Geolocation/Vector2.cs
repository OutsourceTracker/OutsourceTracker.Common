using OutsourceTracker.Geolocation.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace OutsourceTracker.Geolocation;

/// <summary>
/// Represents a geographic coordinate point with latitude, longitude
/// This struct is used in the OutsourceTracker application for storing and managing location data,
/// particularly for equipment tracking, enabling precise geolocation features such as mapping and proximity calculations.
/// </summary>
[JsonConverter(typeof(Vector2JsonConverter))]
public readonly struct Vector2 : IEquatable<Vector2>
{
    /// <summary>
    /// Gets the latitude component of the coordinate in degrees.
    /// </summary>
    public double X { get; init; }

    /// <summary>
    /// Gets the longitude component of the coordinate in degrees.
    /// </summary>
    public double Y { get; init; }

    public Vector2(double x, double y)
    {
        X = x;
        Y = y;
    }

    #region Static Members

    /// <summary>
    /// Gets a static instance representing a zero or default coordinate (0.0 latitude, 0.0 longitude).
    /// This can be used as a sentinel value or initial state in location tracking scenarios.
    /// </summary>
    public static Vector2 Zero { get; } = new Vector2(0, 0);

    /// <summary>
    /// Gets a static instance representing a zero or default coordinate (-1.0 latitude, 0.0 longitude).
    /// This can be used as a sentinel value or initial state in location tracking scenarios.
    /// </summary>
    public static Vector2 Left { get; } = new Vector2(-1, 0);

    /// <summary>
    /// Gets a static instance representing a zero or default coordinate (1.0 latitude, 0.0 longitude).
    /// This can be used as a sentinel value or initial state in location tracking scenarios.
    /// </summary>
    public static Vector2 Right { get; } = new Vector2(1, 0);

    /// <summary>
    /// Gets a static instance representing a zero or default coordinate (0.0 latitude, 1.0 longitude).
    /// This can be used as a sentinel value or initial state in location tracking scenarios.
    /// </summary>
    public static Vector2 Forward { get; } = new Vector2(0, 1);

    /// <summary>
    /// Gets a static instance representing a zero or default coordinate (0.0 latitude, -1.0 longitude).
    /// This can be used as a sentinel value or initial state in location tracking scenarios.
    /// </summary>
    public static Vector2 Backward { get; } = new Vector2(0, -1);

    /// <summary>
    /// Gets the calculated distance between two Vector2 points using the Euclidean distance formula.
    /// </summary>
    /// <param name="a">Point A</param>
    /// <param name="b">Point B</param>
    /// <returns>The distance</returns>
    public static double Distance(Vector2 a, Vector2 b)
    {
        double dx = a.X - b.X;
        double dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    #endregion

    #region Overrides

    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Vector2 other && this == other;

    public bool Equals(Vector2 other) => this == other;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    #endregion

    #region Operators

    public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);

    public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);

    public static bool operator ==(Vector2 a, Vector2 b) => a.X == b.X && a.Y == b.Y;

    public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

    #endregion
}
