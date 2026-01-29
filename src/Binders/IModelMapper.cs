using OutsourceTracker.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OutsourceTracker.Binders;

public interface IModelMapper<TObject, TID, TModel> : IObjectMapper<TObject, TModel> where TModel : IModel<TID>
{
}
