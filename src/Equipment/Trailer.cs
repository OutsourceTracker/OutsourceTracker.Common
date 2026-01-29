using OutsourceTracker.Accounts;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutsourceTracker.Equipment;

public class Trailer : ITrailer<Guid>
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public string Prefix { get; set; }

    public string CallSign { get; set; }

    public string Name { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }
    
    public Guid AccountId { get; set; }

    [ForeignKey(nameof(AccountId))]
    public BusinessAccount? Account { get; set; }

    public override string ToString() => Name;
}
