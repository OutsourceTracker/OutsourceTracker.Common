namespace OutsourceTracker.Services.ModelService;

/// <summary>
/// Defines a generic create service interface for creating data models.
/// This interface provides asynchronous methods for creating data models.
/// </summary>
/// <typeparam name="TID">The type of the identifier for the data model (e.g., int, Guid).</typeparam>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel{TID}"/>.</typeparam>
/// <typeparam name="TReturn">The type of the result returned by the create operation, which can be the created model, a status object, or any relevant data.</typeparam>
public interface IModelCreateService<TID, TModel, TReturn> where TID : struct where TModel : IServiceModel<TID>
{
    /// <summary>
    /// Asynchronously creates a new data model instance.
    /// </summary>
    /// <param name="cancellationToken">An optional cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the identifier of the newly created model, or null if creation failed.</returns>
    Task<TReturn> Create(CancellationToken cancellationToken = default);
}

/// <summary>
/// Defines a generic create service interface for creating data models.
/// This interface provides asynchronous methods for creating data models.
/// </summary>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel{Guid}"/>.</typeparam>
/// <typeparam name="TReturn">The type of the result returned by the create operation, which can be the created model, a status object, or any relevant data.</typeparam>
public interface IModelCreateService<TModel, TReturn> : IModelCreateService<Guid, TModel, TReturn> where TModel : IServiceModel<Guid> { }
