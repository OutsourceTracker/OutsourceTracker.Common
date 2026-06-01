using OutsourceTracker.Geolocation;
using OutsourceTracker.Services.ModelService;

namespace OutsourceTracker.BusinessUnit.Zones;

/// <summary>
/// Concrete Zone model for API DTOs and frontend use. Implements the IZone contract from Geolocation.
/// Placed under BusinessUnit to match the pattern of OrganizationalAccount / OrganizationalUnit and avoid
/// type name conflicts with the backend's internal OutsourceTracker.Models.Zones.Zone entity.
/// </summary>
public class Zone : IZone<Guid>
{
    public Guid Id { get; set; }

    public string ShortCode { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public Polygon Boundry { get; set; }

    public ICollection<Vector2> EntryPoints { get; set; } = new List<Vector2>();

    public ICollection<Vector2> ExitPoints { get; set; } = new List<Vector2>();

    public ICollection<Vector2> DockPoints { get; set; } = new List<Vector2>();

    /// <summary>
    /// Collection of preferred locations (pools) within the zone for placing trailers/equipment.
    /// Used as suggested/default coordinates when this zone is chosen in location update dialogs.
    /// </summary>
    public ICollection<Vector2> TrailerPools { get; set; } = new List<Vector2>();

    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

    public bool Equals(Guid other) => Id.Equals(other);

    public override string ToString() => string.IsNullOrWhiteSpace(ShortCode)
        ? FullName
        : $"{ShortCode} - {FullName}";
}
