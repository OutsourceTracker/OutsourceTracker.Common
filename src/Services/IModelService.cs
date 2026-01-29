namespace OutsourceTracker.Services;

public interface IModelService<TID, TModel> where TModel : IModel<TID>
{
    Task<TModel> AddModel(Action<TModel> modelCreateCallback, CancellationToken cancellationToken = default);

    Task<TModel?> GetModel(TID id, CancellationToken cancellationToken = default);

    Task<bool> DeleteModel(TID id, TModel model, CancellationToken cancellationToken = default);

    Task<TModel> UpdateModel(TID id, Func<TModel, bool> modelUpdateCallback, CancellationToken cancellationToken = default);
}

public interface IModelService<TModel> : IModelService<Guid, TModel> where TModel : IModel<Guid>
{
}