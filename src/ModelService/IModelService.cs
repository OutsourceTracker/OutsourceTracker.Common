namespace OutsourceTracker.ModelService;

public interface IModelService<TID, TModel, TFindRequest> where TModel : IServiceModel<TID>
{
    Task<TModel?> Get(TID id, CancellationToken cancellationToken = default);

    IAsyncEnumerable<TModel> Find(TFindRequest? request = default, CancellationToken cancellationToken = default);
}