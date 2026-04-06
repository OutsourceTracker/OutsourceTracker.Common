using Microsoft.EntityFrameworkCore;
using OutsourceTracker.Geolocation;
using System.ComponentModel.DataAnnotations;

namespace OutsourceTracker.Equipment.Trailers;

[Index(nameof(Prefix), IsUnique = false, Name = "IX_Trailer_Prefix")]
[Index(nameof(Name), IsUnique = false, Name = "IX_Trailer_Name")]
[Index(nameof(FullName), IsUnique = true, Name = "IX_Trailer_FullName_Unique")]
public class TrailerModel : ITrailer<Guid>
{
    [Key]
    public Guid Id { get; set; } = default!;

    public string Prefix { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public TrailerType Type { get; set; } = TrailerType.Van;

    public EquipmentState State { get; set; } = EquipmentState.Available;

    public DateTimeOffset CreatedOn { get; set; } = default!;

    public Guid? AccountId { get; set; }

    public string? LocatedBy { get; set; }

    public DateTimeOffset? LocatedDate { get; set; }

    public Vector2? Location { get; set; }

    public double? LocationAccuracy { get; set; }

    public Guid? ZoneId { get; set; }

    public string? ZoneName { get; set; }
}
