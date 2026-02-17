namespace OutsourceTracker.Services.ModelService;

/// <summary>
/// Defines a generic delete service interface for deleting data models.
/// This interface provides asynchronous methods for deleting data models.
/// </summary>
/// <typeparam name="TID">The type of the identifier for the data model (e.g., int, Guid).</typeparam>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel{TID}"/>.</typeparam>
public interface IModelDeleteService<TID, TModel> where TID : struct where TModel : IServiceModel<TID>
{
    /// <summary>
    /// Asynchronously deletes a data model by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the model to delete.</param>
    /// <param name="cancellationToken">An optional cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing true if the deletion was successful, false otherwise.</returns>
    Task<bool> Delete(TID id, CancellationToken cancellationToken = default);
}

/// <summary>
/// Defines a generic delete service interface for deleting data models.
/// This interface provides asynchronous methods for deleting data models.
/// </summary>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel"/>.</typeparam>
public interface IModelDeleteService<TModel> : IModelDeleteService<Guid, TModel> where TModel : IServiceModel<Guid> { }
