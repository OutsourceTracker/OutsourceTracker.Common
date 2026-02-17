namespace OutsourceTracker.Services.ModelService;

/// <summary>
/// Defines a generic query service interface for retrieving data models in the OutsourceTracker application.
/// This interface provides asynchronous methods for fetching individual models by ID and searching for multiple models
/// based on optional search criteria, supporting efficient data access and retrieval operations in the backend API.
/// </summary>
/// <typeparam name="TID">The type of the identifier for the data model (e.g., int, Guid).</typeparam>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel{TID}"/>.</typeparam>
/// <typeparam name="TSearchOptions">The type of the search options used to filter or parameterize the search query.</typeparam>
public interface IModelLookupService<TID, TModel, TSearchOptions> where TModel : IServiceModel<TID> where TID : struct
{
    /// <summary>
    /// Asynchronously retrieves a single data model by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the model to retrieve.</param>
    /// <param name="cancellationToken">An optional cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the model if found, or null if not found.</returns>
    Task<TModel?> Get(TID id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously searches for data models based on the provided search options, returning an asynchronous enumerable sequence.
    /// This method supports streaming results for large datasets, improving performance in API responses.
    /// </summary>
    /// <param name="searchOptions">Optional search criteria to filter the results.</param>
    /// <param name="cancellationToken">An optional cancellation token to cancel the operation.</param>
    /// <returns>An asynchronous enumerable sequence of matching data models.</returns>
    IAsyncEnumerable<TModel> Search(TSearchOptions? searchOptions = default, CancellationToken cancellationToken = default);
}

/// <summary>
/// Defines a generic query service interface for retrieving data models in the OutsourceTracker application.
/// This interface provides asynchronous methods for fetching individual models by ID and searching for multiple models
/// based on optional search criteria, supporting efficient data access and retrieval operations in the backend API.
/// </summary>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel"/>.</typeparam>
/// <typeparam name="TSearchOptions">The type of the search options used to filter or parameterize the search query.</typeparam>
public interface IModelLookupService<TModel, TSearchOptions> : IModelLookupService<Guid, TModel, TSearchOptions> where TModel : IServiceModel<Guid> { }

/// <summary>
/// Defines a generic query service interface for retrieving data models in the OutsourceTracker application.
/// This interface provides asynchronous methods for fetching individual models by ID and searching for multiple models
/// based on optional search criteria, supporting efficient data access and retrieval operations in the backend API.
/// </summary>
/// <typeparam name="TModel">The type of the data model, which must implement <see cref="IServiceModel"/>.</typeparam>
public interface IModelLookupService<TModel> : IModelLookupService<TModel, object> where TModel: IServiceModel<Guid> { }