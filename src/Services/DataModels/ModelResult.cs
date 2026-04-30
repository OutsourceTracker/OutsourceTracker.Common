namespace OutsourceTracker.Services.DataModels
{
    /// <summary>
    /// Represents the result of a model/service operation in a consistent, immutable way.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This ref struct is the standard return type for services that follow the 
    /// pattern (CRUD services, validation services, business logic operations, etc.)
    /// </para>
    /// </remarks>
    public readonly struct ModelResult
    {
        /// <summary>
        /// Indicates whether the operation completed successfully.
        /// </summary>
        /// <value><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</value>
        public bool Success { get; }

        /// <summary>
        /// Optional dictionary of validation or business rule errors.
        /// </summary>
        /// <remarks>
        /// Key = property/field name (or a general key such as "_general" or "_model").
        /// Value = error message or object with additional context.
        /// 
        /// This collection is typically used by the frontend (Blazor) to display 
        /// field-specific validation messages.
        /// </remarks>
        public IReadOnlyDictionary<string, object>? Errors { get; }

        /// <summary>
        /// The payload/data returned by the successful operation.
        /// </summary>
        /// <remarks>
        /// Can be a domain model, DTO, collection, scalar value, or <c>null</c>.
        /// Consumers should cast or pattern-match based on the calling context.
        /// </remarks>
        public object? Data { get; }

        /// <summary>
        /// Internal constructor. Use the <see cref="Builder"/> factory for public construction.
        /// </summary>
        internal ModelResult(bool success, IReadOnlyDictionary<string, object>? errors = null, object? data = null)
        {
            Success = success;
            Errors = errors;
            Data = data;
        }

        /// <summary>
        /// Creates a new fluent builder for constructing <see cref="ModelResult"/> instances.
        /// </summary>
        /// <returns>A fresh <see cref="IModelResultBuilder"/> instance.</returns>
        public static IModelResultBuilder Builder() => new ModelResultBuilder();
    }
}