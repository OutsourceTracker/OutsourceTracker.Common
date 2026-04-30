namespace OutsourceTracker.Services.DataModels
{
    /// <inheritdoc />
    internal class ModelResultBuilder : IModelResultBuilder
    {
        private bool HasSuccess = false;
        private IDictionary<string, object>? Errors;
        private object? Result;

        /// <inheritdoc />
        public IModelResultBuilder ClearErrors()
        {
            lock (this)
            {
                if (Errors != null)
                {
                    Errors.Clear();
                }
            }

            return this;
        }

        /// <inheritdoc />
        public IModelResultBuilder AddError(string errorKey, object errorObject)
        {
            ArgumentNullException.ThrowIfNull(errorKey, nameof(errorKey));
            ArgumentNullException.ThrowIfNull(errorObject, nameof(errorObject));

            lock (this)
            {
                Errors ??= new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                Errors[errorKey] = errorObject;
            }

            return this;
        }

        /// <inheritdoc />
        public IModelResultBuilder AddErrors(IDictionary<string, object> errors)
        {
            ArgumentNullException.ThrowIfNull(errors, nameof(errors));

            if (errors.Count == 0)
            {
                return this;
            }

            lock (this) 
            {
                Errors ??= new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                
                foreach (var error in errors)
                {
                    Errors.Add(error.Key, error.Value);
                }
            }

            return this;
        }

        /// <inheritdoc />
        public IModelResultBuilder WithResult(object result)
        {
            Result = result;
            return this;
        }

        /// <inheritdoc />
        public IModelResultBuilder WithSuccess()
        {
            HasSuccess = true;
            return this;
        }

        /// <inheritdoc />
        public ModelResult Build() => new(HasSuccess, Errors?.AsReadOnly(), Result);

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}
