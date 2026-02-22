using OutsourceTracker.Geolocation.Converters;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace OutsourceTracker.Geolocation;

/// <summary>
/// A simple, lightweight, immutable 2D polygon struct for defining zones.
/// Points are stored in counter-clockwise or clockwise order (ray-casting works either way).
/// First and last points are NOT required to be identical (we close it implicitly).
/// </summary>
[JsonConverter(typeof(PolygonJsonConverter))]
public readonly struct Polygon : IEquatable<Polygon>
{
    /// <summary>
    /// The vertices of the polygon (X = longitude/easting, Y = latitude/northing or your coordinate system).
    /// </summary>
    public readonly ReadOnlyMemory<Vector2> Points { get; init; }

    /// <summary>
    /// Number of vertices in the polygon.
    /// </summary>
    public int VertexCount => Points.Length;

    /// <summary>
    /// True if the polygon has at least 3 points and forms a valid simple polygon (basic check).
    /// </summary>
    public bool IsValid => VertexCount >= 3 && !HasDuplicateConsecutivePoints();

    /// <summary>
    /// Creates a new Polygon from a collection of Vector2 points.
    /// </summary>
    public Polygon(IEnumerable<Vector2> points)
    {
        var arr = points?.ToArray() ?? Array.Empty<Vector2>();
        Points = arr.AsMemory();
    }

    /// <summary>
    /// Creates a new Polygon from a span of Vector2 points.
    /// </summary>
    public Polygon(ReadOnlySpan<Vector2> points)
    {
        Points = points.ToArray().AsMemory();
    }

    /// <summary>
    /// Point-in-polygon test using ray-casting (even-odd rule).
    /// Boundary points are considered inside.
    /// </summary>
    /// <returns>true if the point is inside or on the boundary of the polygon</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Vector2 point)
    {
        if (!IsValid) return false;

        bool inside = false;
        var span = Points.Span;

        for (int i = 0, j = VertexCount - 1; i < VertexCount; j = i++)
        {
            Vector2 a = span[i];
            Vector2 b = span[j];

            if (a.Y == b.Y) continue;

            if (a.Y > b.Y)
            {
                (a, b) = (b, a);
            }

            if (point.Y <= a.Y || point.Y > b.Y) continue;

            double dx = b.X - a.X;
            double dy = b.Y - a.Y;
            double t = (point.Y - a.Y) / dy;
            double xinters = a.X + t * dx;

            if (xinters >= point.X)
            {
                inside = !inside;
            }
        }

        return inside;
    }

    /// <summary>
    /// Checks if this polygon is approximately equal to another (within epsilon).
    /// </summary>
    public bool Equals(Polygon other, float epsilon = 0.0001f)
    {
        if (VertexCount != other.VertexCount) return false;
        var s1 = Points.Span;
        var s2 = other.Points.Span;
        for (int i = 0; i < VertexCount; i++)
        {
            if (Vector2.Distance(s1[i], s2[i]) > epsilon)
                return false;
        }
        return true;
    }

    private bool HasDuplicateConsecutivePoints()
    {
        if (VertexCount < 2) return false;
        var span = Points.Span;
        for (int i = 0; i < VertexCount - 1; i++)
        {
            if (span[i] == span[i + 1])
                return true;
        }

        return span[^1] == span[0];
    }

    #region Operators and Overrides

    public override bool Equals(object? obj) => obj is Polygon other && Equals(other);

    public bool Equals(Polygon other) => Equals(other, 0.0001f);

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (var p in Points.Span)
            {
                hash = hash * 31 + p.GetHashCode();
            }
            return hash;
        }
    }

    public override string ToString() => $"Polygon ({VertexCount} vertices)";

    public static bool operator ==(Polygon left, Polygon right) => left.Equals(right);
    public static bool operator !=(Polygon left, Polygon right) => !left.Equals(right);

    #endregion
}
