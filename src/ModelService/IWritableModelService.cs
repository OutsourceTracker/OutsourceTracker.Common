namespace OutsourceTracker.ModelService;

public interface IWritableModelService<TID, TModel, TCreateRequest, TUpdateRequest, TDeleteRequest> where TModel : IServiceModel<TID>
{
    Task<TID?> Create(TCreateRequest? request, CancellationToken cancellationToken = default);

    Task<TModel?> Update(TID id, TUpdateRequest? request, CancellationToken cancellationToken = default);

    ValueTask<bool> Delete(TID id, TDeleteRequest? request, CancellationToken cancellationToken = default);
}
