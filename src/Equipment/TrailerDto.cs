using System;
using System.Collections.Generic;
using System.Text;

namespace OutsourceTracker.Equipment;

public class TrailerDto : EquipmentDto
{
    public string? Prefix { get; set; }

    public string? CallSign { get; set; }
}
