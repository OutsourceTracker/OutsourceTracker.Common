using System;
using System.Collections.Generic;
using System.Text;

namespace OutsourceTracker.Services;

public interface IModel<TID>
{
    TID Id { get; set; }
}
