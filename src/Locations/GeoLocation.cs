namespace OutsourceTracker.Locations;

public class GeoLocation : IGeoLocation<Guid>
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string ShortCode { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string FullAddress { get; set; }
}
