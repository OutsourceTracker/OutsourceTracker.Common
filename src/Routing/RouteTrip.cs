using System.ComponentModel.DataAnnotations.Schema;

namespace OutsourceTracker.Routing;

public class RouteTrip : IRoute<Guid>
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public string LoadName { get; set; }

    public Guid AccountId { get; set; }

    public Guid DriverId { get; set; }

    public DateTimeOffset Date { get; set; }
    public int Stops { get; set; }

    public int TotalMiles { get; set; }

    public ICollection<RouteStop> TripSheet { get; set; } = new List<RouteStop>();
}
