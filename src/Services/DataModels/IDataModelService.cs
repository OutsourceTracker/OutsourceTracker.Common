namespace OutsourceTracker.Services.DataModels;

/// <summary>
/// Generic base interface for all data model CRUD services in OutsourceTracker.
/// </summary>
/// <typeparam name="TID">Type of the model's unique identifier (e.g. <see cref="int"/>, <see cref="Guid"/>).</typeparam>
/// <typeparam name="TModel">The domain model or DTO type this service operates on.</typeparam>
/// <remarks>
/// <para>
/// This interface defines the standard contract that all concrete model services must implement.
/// It follows the <see cref="ModelResult"/> pattern for consistent success/failure handling across the entire backend.
/// </para>
/// <para>
/// All methods are asynchronous and support cancellation tokens for better scalability in Blazor WebAssembly + API scenarios.
/// </para>
/// </remarks>
public interface IDataModelService<TID, TModel> where TModel : class where TID : struct
{
    /// <summary>
    /// Creates a new model instance.
    /// </summary>
    /// <typeparam name="T">Type of the incoming creation parameters (often a DTO or command object).</typeparam>
    /// <param name="modelParameters">Data needed to create the new model. Can be null for default creation.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ModelResult"/> containing the newly created model in <see cref="ModelResult.Data"/> on success,
    /// or validation/business errors on failure.
    /// </returns>
    Task<ModelResult> Create<T>(T? modelParameters = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing model by its ID.
    /// </summary>
    /// <typeparam name="T">Type of the update parameters (often a DTO or patch object).</typeparam>
    /// <param name="modelId">Unique identifier of the model to update.</param>
    /// <param name="modelParameters">Updated values for the model.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ModelResult"/> with the updated model in <see cref="ModelResult.Data"/> on success.
    /// </returns>
    Task<ModelResult> Update<T>(TID modelId, T modelParameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft or hard deletes a model by its ID.
    /// </summary>
    /// <param name="modelID">Unique identifier of the model to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ModelResult"/> indicating success. <see cref="ModelResult.Data"/> may contain 
    /// the deleted model or a confirmation object.
    /// </returns>
    Task<ModelResult> Delete(TID modelID, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a single model by its ID.
    /// </summary>
    /// <param name="modelId">Unique identifier of the model.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ModelResult"/> with the requested model in <see cref="ModelResult.Data"/> if found.
    /// Returns failure with appropriate errors if not found or access denied.
    /// </returns>
    Task<ModelResult> Get(TID modelId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for models using flexible search parameters.
    /// </summary>
    /// <typeparam name="T">Type of the search criteria (search DTO, filter object, etc.).</typeparam>
    /// <param name="searchParameters">Search, filter, paging, and sorting parameters.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ModelResult"/> where <see cref="ModelResult.Data"/> typically contains 
    /// a collection of models, total count, and pagination metadata on success.
    /// </returns>
    Task<ModelResult> Search<T>(T? searchParameters = default, CancellationToken cancellationToken = default);
}