namespace OutsourceTracker.Equipment.Trailers;

/// <summary>
/// Defines a trailer interface that extends the base equipment model.
/// This interface represents specific trailer equipment in the OutsourceTracker application,
/// including prefix for identification, full naming, and type classification to support
/// specialized tracking, dispatching, and reporting for trailer assets.
/// </summary>
/// <typeparam name="TID">The type of the identifier inherited from <see cref="IEquipment{TID}"/> (e.g., int, Guid).</typeparam>
public interface ITrailer<TID> : IEquipment<TID> where TID : struct
{
    /// <summary>
    /// Gets the prefix used for the trailer's identifier or code (e.g., a shorthand or category prefix).
    /// </summary>
    string Prefix { get; }

    /// <summary>
    /// Gets the full name or descriptive title of the trailer.
    /// </summary>
    string FullName { get; }

    /// <summary>
    /// Gets the type of the trailer, categorizing it based on configuration and use (e.g., Van, Reefer).
    /// </summary>
    TrailerType Type { get; }
}