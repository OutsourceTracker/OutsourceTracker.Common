using OutsourceTracker.Accounts;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutsourceTracker.Users.Drivers;

public class Driver<TID> : User<TID>, IDriver<TID>
{
    public string AlphaCode { get; set; } = string.Empty;

    public Guid AccountId { get; set; }

    [ForeignKey(nameof(AccountId))]
    public BusinessAccount Account { get; set; }

    public override string ToString() => $"[{AlphaCode}] {base.ToString()}";

    public override int GetHashCode() => HashCode.Combine(Id, WorkdayId, AlphaCode);
}

public class Driver : Driver<Guid>
{
    public Driver()
    {
        Id = Guid.CreateVersion7();
    }
}