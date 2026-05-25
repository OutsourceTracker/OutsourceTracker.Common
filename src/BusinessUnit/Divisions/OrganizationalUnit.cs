using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OutsourceTracker.BusinessUnit.Divisions;

[Index(nameof(ShortCode), IsUnique = true, Name = "IX_OrganizationalUnit_ShortCode_Unique")]
[Index(nameof(Name), IsUnique = false, Name = "IX_OrganizationalUnit_Name")]
public class OrganizationalUnit : IOrganizationalUnit<Guid>
{
    [Key]
    public Guid Id { get; set; } = default!;

    public string ShortCode { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Description { get; set;  } = default!;

    public int TotalAccounts { get; set; } = 0;

    public DateTimeOffset CreatedOn { get; set; } = default!;

    public bool Equals(Guid other) => Id.Equals(other);

    public override string ToString() => ShortCode;
}
