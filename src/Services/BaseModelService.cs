using Microsoft.Extensions.Logging;

namespace OutsourceTracker.Services;

public abstract class BaseModelService<TModel, TDtoModel> : IModelService<Guid, TModel, TDtoModel> where TModel : IModel<Guid>, new() where TDtoModel : class, new()
{
    protected virtual string ModelName { get; }

    protected ILogger Log { get; }

    protected BaseModelService(ILogger logger)
    {
        ModelName = typeof(TModel).Name;
        Log = logger;
    }

    public async Task<TModel?> AddModel(Action<TDtoModel> modelCreateCallback, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(modelCreateCallback, nameof(modelCreateCallback));
        DateTimeOffset now = DateTimeOffset.Now;
        Guid id = Guid.CreateVersion7(now);
        TDtoModel dto = new();
        TModel model = new TModel()
        {
            Id = id
        };
        modelCreateCallback(dto);
        if (ProcessChanges(dto, model))
        {
            NormalizeModel(model);
            await AddModel(model, cancellationToken);
            Log.LogInformation("Successfully added {MODEL_NAME} {MODEL_VALUE}", ModelName, model);
            return model;
        }
        Log.LogWarning("No changes were made when attempting to add new {MODEL_NAME}, skipping database insert", ModelName);
        return default;
    }

    public async Task<TModel> UpdateModel(Guid id, TModel model, Func<TDtoModel, bool> modelUpdateCallback, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(modelUpdateCallback, nameof(modelUpdateCallback));

        if (model == null || model.Id != id)
        {
            Log.LogError("The provided {MODEL_NAME} ID does not match the {MODEL_NAME}'s ID.", ModelName);
            throw new ArgumentException($"The provided {ModelName} ID does not match the {ModelName}'s ID.", nameof(id));
        }

        TDtoModel dto = new();
        CopyToDto(model, dto);
        bool result = modelUpdateCallback(dto);

        if (result && ProcessChanges(dto, model))
        {
            NormalizeModel(model);
            model = await UpdateModel(model, cancellationToken);
            Log.LogInformation("Successfully updated {MODEL_NAME} {MODEL} in the database", ModelName, model);
        }
        else
        {
            Log.LogInformation("No changes were made when attempting to update {MODEL_NAME} {MODEL}, skipping database update", ModelName, model);
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

    protected abstract bool ProcessChanges(TDtoModel dto, TModel model);

    protected abstract bool CopyToDto(TModel model, TDtoModel dto);

    #region Database Calls

    protected abstract Task<TModel> AddModel(TModel model, CancellationToken cancellationToken = default);

    protected abstract Task<bool> DeleteModel(TModel model, CancellationToken cancellationToken = default);

    protected abstract Task<TModel> UpdateModel(TModel model, CancellationToken cancellationToken = default);

    #endregion
}
