using Microsoft.Extensions.Logging;

namespace OutsourceTracker.Services;

public abstract class BaseModelService<TModel> : IModelService<Guid, TModel> where TModel : IModel<Guid>, new()
{
    protected virtual string ModelName { get; }

    protected ILogger Log { get; }

    protected BaseModelService(ILogger logger)
    {
        ModelName = typeof(TModel).Name;
        Log = logger;
    }

    public async Task<TModel> AddModel(Action<TModel> modelCreateCallback, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(modelCreateCallback, nameof(modelCreateCallback));
        DateTimeOffset now = DateTimeOffset.Now;
        Guid id = Guid.CreateVersion7(now);
        TModel model = new TModel()
        {
            Id = id
        };
        modelCreateCallback(model);
        model.Id = id;
        NormalizeModel(model);
        await AddModel(model, cancellationToken);
        Log.LogInformation("Successfully added {MODEL_NAME} {MODEL_VALUE}", ModelName, model);
        return model;
    }

    public async Task<TModel> UpdateModel(Guid id, Func<TModel, bool> modelUpdateCallback, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(modelUpdateCallback, nameof(modelUpdateCallback));
        TModel? model = await GetModel(id, cancellationToken);

        if (model == null)
        {
            Log.LogError("Attempted to update {MODEL_NAME} {ID} but entry doesn't exist", ModelName, id);
            throw new KeyNotFoundException($"{ModelName} with ID {id} was not found.");
        }

        Guid originalId = model.Id;
        bool result = modelUpdateCallback(model);

        if (result)
        {
            model.Id = originalId;
            NormalizeModel(model);
            model = await UpdateModel(model, cancellationToken);
            Log.LogInformation("Successfully updated {MODEL_NAME} {MODEL} in the database", ModelName, model);
        }

        return model;
    }

    public async Task<bool> DeleteModel(Guid id, TModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        if (model.Id != id)
        {
            throw new ArgumentException($"The provided {ModelName} ID does not match the {ModelName}'s ID.", nameof(id));
        }

        if (await DeleteModel(model, cancellationToken))
        {
            Log.LogInformation("Successfully deleted {MODEL_NAME} {MODEL_VALUE}.", ModelName, model);
            return true;
        }
        else
        {
            Log.LogWarning("Failed to delete driver {DRIVER} from the database", model);
            return false;
        }
    }

    public abstract Task<TModel?> GetModel(Guid id, CancellationToken cancellationToken = default);

    protected abstract void NormalizeModel(TModel model);

    #region Database Calls

    protected abstract Task<TModel> AddModel(TModel model, CancellationToken cancellationToken = default);

    protected abstract Task<bool> DeleteModel(TModel model, CancellationToken cancellationToken = default);

    protected abstract Task<TModel> UpdateModel(TModel model, CancellationToken cancellationToken = default);

    #endregion
}
