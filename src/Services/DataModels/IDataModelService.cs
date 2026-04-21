using OutsourceTracker.Services.ModelService;

namespace OutsourceTracker.Services.DataModels;

public interface IDataModelService<TID, TModel, TResult> where TModel : IServiceModel<TID> where TID : struct where TResult : IModelResult<TID, TModel>
{
    TResult Create<T>(T? modelParameters = default, CancellationToken cancellationToken = default);

    TResult Update<T>(TID modelId, T? modelParameters, CancellationToken cancellationToken = default);

    TResult Delete(TID modelID, CancellationToken cancellationToken = default);

    TResult Get(TID modelId, CancellationToken cancellationToken = default);

    TResult Search<T>(T? searchParameters = default, CancellationToken cancellationToken = default);
}
