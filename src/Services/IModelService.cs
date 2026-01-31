namespace OutsourceTracker.Services;

public interface IModelService<TID, TModel, TDtoModel> where TModel : IModel<TID> where TDtoModel : class
{
    Task<TModel?> AddModel(Action<TDtoModel> modelCreateCallback, CancellationToken cancellationToken = default);

    Task<TModel?> GetModel(TID id, CancellationToken cancellationToken = default);

    Task<bool> DeleteModel(TID id, TModel model, CancellationToken cancellationToken = default);

    Task<TModel> UpdateModel(TID id, TModel model, Func<TDtoModel, bool> modelUpdateCallback, CancellationToken cancellationToken = default);
}

public interface IModelService<TModel, TDtoModel> : IModelService<Guid, TModel, TDtoModel> where TModel : IModel<Guid> where TDtoModel : class
{
}