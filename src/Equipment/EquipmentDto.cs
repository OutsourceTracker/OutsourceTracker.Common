namespace OutsourceTracker.Equipment;

public class EquipmentDto
{
    public string? Name { get; set; }

    public Guid? Account { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }
}
