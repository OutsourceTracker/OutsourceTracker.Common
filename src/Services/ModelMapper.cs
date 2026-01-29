using OutsourceTracker.Binders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OutsourceTracker.Services;

public class ModelMapper<TObject, TModel> : IModelMapper<TObject, Guid, TModel> where TModel : IModel<Guid>
{


    public TModel Update(TObject source, TModel destination)
    {
        throw new NotImplementedException();
    }
}
