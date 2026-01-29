namespace OutsourceTracker.Accounts;

public class OrganizationalUnit : IOrganizationalDivision<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string ShortCode { get; set; }

    public string? Description { get; set; }

    public OrganizationalUnit()
    {
        Id = Guid.CreateVersion7();
    }
}
