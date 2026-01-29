using System.ComponentModel.DataAnnotations.Schema;

namespace OutsourceTracker.Accounts;

public class BusinessAccount : IAccount<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string LocationCode { get; set; }

    public Guid DivisionId { get; set; }

    [ForeignKey(nameof(DivisionId))]
    public OrganizationalUnit Division { get; set; }

    public BusinessAccount()
    {
        Id = Guid.CreateVersion7();
    }
}
