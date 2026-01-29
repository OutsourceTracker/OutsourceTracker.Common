using OutsourceTracker.Services;

namespace OutsourceTracker.Routing;

public interface IRouteStop<TID> : IModel<TID>
{
    string Name { get; set; }

    string LocationCode { get; set; }

    string Address { get; set; }

    int Sequence { get; set; }

    TID TripId { get; set; }

    DateTimeOffset? ArrivalTime { get; set; }

    DateTimeOffset? DepartureTime { get; set; }

    decimal? OdometerAtArrival { get; set; }
}
