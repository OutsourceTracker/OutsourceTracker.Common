namespace OutsourceTracker.Services.ModelService;

/// <summary>
/// Defines a data model interface for entities with a unique identifier.
/// This interface ensures that implementing classes have an identifiable key,
/// which is useful for database operations, tracking, and entity management in the OutsourceTracker application.
/// </summary>
/// <typeparam name="TID">The type of the identifier (e.g., int, Guid).</typeparam>
public interface IServiceModel<TID> where TID : struct
{
    /// <summary>
    /// Gets the unique identifier for the data model instance.
    /// </summary>
    TID Id { get; }

    /// <summary>
    /// Gets the timestamp when this model record was first created in the system.
    /// </summary>
    DateTimeOffset CreatedOn { get; }
}