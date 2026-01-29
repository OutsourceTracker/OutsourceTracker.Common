using OutsourceTracker.Services;

namespace OutsourceTracker.Locations;

public interface IGeoLocation<GID> : IModel<GID>
{
    string ShortCode { get; set; }

    string FullName { get; set; }

    string Address { get; set; }

    string City { get; set; }

    string State { get; set; }

    string ZipCode { get; set; }
}
