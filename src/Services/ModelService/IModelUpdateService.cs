using OutsourceTracker.Data;

namespace OutsourceTracker.Services.ModelService;

/// <summary>
/// Defines a generic update service interface for managing data models in the OutsourceTracker application.
/// This interface provides asynchronous methods for creating, deleting, and updating data models,
/// supporting CRUD operations in the backend API with optional cancellation support for long-running tasks.
/// </summary>
/// <typeparam name="TID">The type of the identifier for the data model (e.g., int, Guid).</typeparam>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IDataModel{TID}"/>.</typeparam>
/// <typeparam name="TUpdateRequest">The type of the request object used for update operations, containing the data to apply.</typeparam>
public interface IModelUpdateService<TID, TModel, TUpdateRequest> where TModel : IDataModel<TID> where TID : struct
{
    /// <summary>
    /// Asynchronously creates a new data model instance.
    /// </summary>
    /// <param name="cancellationToken">An optional cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the identifier of the newly created model, or null if creation failed.</returns>
    Task<TID?> Create(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes a data model by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the model to delete.</param>
    /// <param name="cancellationToken">An optional cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing true if the deletion was successful, false otherwise.</returns>
    Task<bool> Delete(TID id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing data model by its unique identifier using the provided update request.
    /// </summary>
    /// <param name="id">The unique identifier of the model to update.</param>
    /// <param name="request">The update request object containing the changes to apply.</param>
    /// <param name="cancellationToken">An optional cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the updated model if successful, or null if not found or update failed.</returns>
    Task<TModel?> Update(TID id, TUpdateRequest request, CancellationToken cancellationToken = default);
}