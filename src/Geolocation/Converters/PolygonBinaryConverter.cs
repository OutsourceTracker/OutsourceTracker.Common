using OutsourceTracker.Converters;

namespace OutsourceTracker.Geolocation.Converters;

public sealed unsafe class PolygonBinaryConverter : IValueConverter<Polygon, byte[]>, IValueConverter<Polygon?, byte[]>
{
    private const int MAX_REASONABLE_POINTS = 5000;
    private const int HEADER_SIZE = sizeof(int);
    private const int POINT_SIZE = sizeof(double) * 2;

    public byte[] ConvertTo(Polygon polygon)
    {
        int count = polygon.VertexCount;
        if (count <= 0 || count > MAX_REASONABLE_POINTS)
        {
            return Array.Empty<byte>();
        }

        int totalBytes = HEADER_SIZE + count * POINT_SIZE;
        byte[] buffer = new byte[totalBytes];

        fixed (byte* ptr = buffer)
        {
            *(int*)ptr = count;

            double* pointPtr = (double*)(ptr + HEADER_SIZE);
            ReadOnlySpan<Vector2> points = polygon.Points.Span;

            for (int i = 0; i < count; i++)
            {
                Vector2 p = points[i];
                pointPtr[0] = p.X;
                pointPtr[1] = p.Y;
                pointPtr += 2;
            }
        }

        return buffer;
    }

    public byte[] ConvertTo(Polygon? obj) => obj.HasValue ? ConvertTo(obj.Value) : Array.Empty<byte>();

    public Polygon ConvertFrom(byte[] bytes)
    {
        if (bytes == null || bytes.Length < HEADER_SIZE)
        {
            return new Polygon(Array.Empty<Vector2>());
        }

        fixed (byte* ptr = bytes)
        {
            int count = *(int*)ptr;

            if (count <= 0 || count > MAX_REASONABLE_POINTS)
            {
                return new Polygon(Array.Empty<Vector2>());
            }

            int expectedBytes = HEADER_SIZE + count * POINT_SIZE;
            if (bytes.Length != expectedBytes)
            {
                return new Polygon(Array.Empty<Vector2>());
            }

            var points = new Vector2[count];
            double* pointPtr = (double*)(ptr + HEADER_SIZE);

            for (int i = 0; i < count; i++)
            {
                points[i] = new Vector2(
                    pointPtr[0],
                    pointPtr[1]
                );
                pointPtr += 2;
            }

            return new Polygon(points);
        }
    }

    

    

    Polygon? IValueConverter<Polygon?, byte[]>.ConvertFrom(byte[] obj) => ConvertFrom(obj);
}
