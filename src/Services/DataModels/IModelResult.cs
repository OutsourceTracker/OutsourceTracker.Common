using OutsourceTracker.Services.ModelService;

namespace OutsourceTracker.Services.DataModels;

public interface IModelResult<TID, TModel> : IDisposable where TID : struct where TModel : IServiceModel<TID>
{
    bool IsSuccess { get; }

    IDictionary<string, object>? Errors { get; }

    object? Data { get; }

    T GetData<T>();
}
