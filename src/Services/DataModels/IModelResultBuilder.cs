namespace OutsourceTracker.Services.DataModels
{
    /// <summary>
    /// Fluent builder interface for constructing <see cref="ModelResult"/> instances.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Provides a clean, chainable API for building success and failure results 
    /// in service layer operations. This pattern is the recommended way to return 
    /// data from services throughout the OutsourceTracker backend.
    /// </para>
    /// <para>
    /// The builder implements <see cref="IDisposable"/> to allow future extensions 
    /// (e.g., pooled builders or resource cleanup) while keeping the API flexible.
    /// </para>
    /// </remarks>
    public interface IModelResultBuilder : IDisposable
    {
        /// <summary>
        /// Clears all existing errors from the builder, resetting it to a clean state.
        /// </summary>
        /// <returns>The current builder instance for fluent chaining.</returns>
        IModelResultBuilder ClearErrors();

        /// <summary>
        /// Adds a single error to the result.
        /// </summary>
        /// <param name="errorKey">The key identifying the field or error category (e.g. "Name", "Email", "_general").</param>
        /// <param name="errorObject">The error message, object, or validation details.</param>
        /// <returns>The current builder instance for fluent chaining.</returns>
        IModelResultBuilder AddError(string errorKey, object errorObject);

        /// <summary>
        /// Adds multiple errors at once.
        /// </summary>
        /// <param name="errors">Dictionary of error keys and their corresponding error objects.</param>
        /// <returns>The current builder instance for fluent chaining.</returns>
        IModelResultBuilder AddErrors(IDictionary<string, object> errors);

        /// <summary>
        /// Sets the successful payload/data of the result.
        /// </summary>
        /// <param name="result">The data to return on success (model, DTO, collection, etc.).</param>
        /// <returns>The current builder instance for fluent chaining.</returns>
        IModelResultBuilder WithResult(object result);

        /// <summary>
        /// Explicitly marks the result as successful.
        /// </summary>
        /// <remarks>
        /// Usually called after <see cref="WithResult(object)"/> but can be used 
        /// for void/success-only operations.
        /// </remarks>
        /// <returns>The current builder instance for fluent chaining.</returns>
        IModelResultBuilder WithSuccess();

        /// <summary>
        /// Builds and returns the final immutable <see cref="ModelResult"/>.
        /// </summary>
        /// <returns>A new <see cref="ModelResult"/> instance.</returns>
        ModelResult Build();
    }
}