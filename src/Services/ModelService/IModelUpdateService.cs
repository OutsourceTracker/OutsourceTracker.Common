namespace OutsourceTracker.Services.ModelService;

/// <summary>
/// Defines a generic update service interface for updating data models.
/// This interface provides asynchronous methods for updating data models,
/// </summary>
/// <typeparam name="TID">The type of the identifier for the data model (e.g., int, Guid).</typeparam>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel{TID}"/>.</typeparam>
/// <typeparam name="TUpdateRequest">The type of the request object used for update operations, containing the data to apply.</typeparam>
/// <typeparam name="TReturn">The type of the result returned by the update operation, which can be the updated model, a status object, or any relevant data.</typeparam>
public interface IModelUpdateService<TID, TModel, TUpdateRequest, TReturn> where TModel : IServiceModel<TID> where TID : struct
{
    /// <summary>
    /// Asynchronously updates an existing data model by its unique identifier using the provided update request.
    /// </summary>
    /// <param name="id">The unique identifier of the model to update.</param>
    /// <param name="request">The update request object containing the changes to apply.</param>
    /// <param name="cancellationToken">An optional cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the updated model if successful, or null if not found or update failed.</returns>
    Task<TReturn> Update(TID id, TUpdateRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Defines a generic update service interface for updating data models.
/// This interface provides asynchronous methods for updating data models,
/// </summary>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel"/>.</typeparam>
/// <typeparam name="TUpdateRequest">The type of the request object used for update operations, containing the data to apply.</typeparam>
/// <typeparam name="TReturn">The type of the result returned by the update operation, which can be the updated model, a status object, or any relevant data.</typeparam>
public interface IModelUpdateService<TModel, TUpdateRequest, TReturn> : IModelUpdateService<Guid, TModel, TUpdateRequest, TReturn> where TModel : IServiceModel<Guid> { }

/// <summary>
/// Defines a generic update service interface for updating data models.
/// This interface provides asynchronous methods for updating data models,
/// </summary>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel"/>.</typeparam>
/// <typeparam name="TReturn">The type of the result returned by the update operation, which can be the updated model, a status object, or any relevant data.</typeparam>
public interface IModelUpdateService<TModel, TReturn> : IModelUpdateService<TModel, object, TReturn> where TModel : IServiceModel<Guid> { }