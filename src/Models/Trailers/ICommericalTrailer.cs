using OutsourceTracker.ModelService;

namespace OutsourceTracker.Models.Trailers;

public interface ICommericalTrailer<TID> : IServiceModel<TID>
{
    string Name { get; }

    string Prefix { get; }

    double? SpottedLatitude { get; }

    double? SpottedLongitude { get; }

    double? SpottedAccuracy { get; }

    string? SpottedBy { get; }

    DateTimeOffset? SpottedOn { get; }
}
