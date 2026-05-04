using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OutsourceTracker.BusinessUnit.Accounts;

[Index(nameof(ShortCode), IsUnique = true, Name = "IX_OrganizationalAccount_ShortCode_Unique")]
[Index(nameof(Name), IsUnique = false, Name = "IX_OrganizationalAccount_Name")]
public class OrganizationalAccount : IOrganizationAccount<Guid>
{
    [Key]
    public Guid Id { get; set; } = default;

    public string ShortCode { get; set; } = default!;

    public string Name { get; set; } = default!;

    public Guid OUID { get; set; } = default;

    public DateTimeOffset CreatedOn { get; set; } = default;

    public bool Equals(Guid other) => Id.Equals(other);
}
