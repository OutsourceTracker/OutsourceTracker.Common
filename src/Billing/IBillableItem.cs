using OutsourceTracker.Services;
using OutsourceTracker.Users.Drivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OutsourceTracker.Billing;

public interface IBillableItem<TID> : IModel<TID>
{
    DateTimeOffset Date { get; set; }

    string Description { get; set; }

    TID DriverId { get; set; }

    TID AccountId { get; set; }


}
