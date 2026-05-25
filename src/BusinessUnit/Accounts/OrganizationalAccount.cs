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

    [RegularExpression(@"^\d{6}$", ErrorMessage = "Cost Center must be in the format XXXXXX")]
    public string? CostCenter { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string? GroupEmail { get; set; }

    public string? Address { get; set; }

    public Guid OUID { get; set; } = default;

    public DateTimeOffset CreatedOn { get; set; } = default;
    

    public bool Equals(Guid other) => Id.Equals(other);

    public override string ToString() => $"{Name} [{ShortCode}]";
}
