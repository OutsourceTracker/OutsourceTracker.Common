using OutsourceTracker.Services;

namespace OutsourceTracker.Routing;

public interface IRoute<TID> : IModel<TID>
{
    string LoadName { get; set; }

    TID AccountId { get; set; }
    
    TID DriverId { get; set; }

    DateTimeOffset Date { get; set; }

    int Stops { get; set; }

    int TotalMiles { get; set; }
}
