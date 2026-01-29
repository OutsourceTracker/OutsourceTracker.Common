using System.ComponentModel.DataAnnotations.Schema;

namespace OutsourceTracker.Routing;

public class RouteStop : IRouteStop<Guid>
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public Guid TripId { get; set; }

    public string Name { get; set; }

    public string LocationCode { get; set; }

    public string Address { get; set; }

    public int Sequence { get; set; }

    public DateTimeOffset? ArrivalTime { get; set; }

    public DateTimeOffset? DepartureTime { get; set; }

    public decimal? OdometerAtArrival { get; set; }
    
}
