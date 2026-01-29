using System;
using System.Collections.Generic;
using System.Text;

namespace OutsourceTracker.Equipment;

public interface ITrailer<TID> : IEquipment<TID>
{
    string Prefix { get; set; }

    string CallSign { get; set; }
}
