using OutsourceTracker.Data;
using OutsourceTracker.Geolocation;

namespace OutsourceTracker.Equipment;

/// <summary>
/// Defines an equipment interface that extends the base data model with properties
/// for tracking physical or rented equipment in the OutsourceTracker application.
/// This interface supports inventory management, location tracking, and accountability
/// by linking equipment to accounts and recording state changes over time.
/// </summary>
/// <typeparam name="TID">The type of the identifier inherited from <see cref="IDataModel{TID}"/> (e.g., int, Guid).</typeparam>
public interface IEquipment<TID> : IDataModel<TID> where TID : struct
{
    /// <summary>
    /// Gets the descriptive name of the equipment item.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the current operational state of the equipment (e.g., Available, InUse, Maintenance, etc.).
    /// </summary>
    EquipmentState State { get; }

    /// <summary>
    /// Gets the optional geographic location (latitude/longitude) where the equipment was last reported.
    /// </summary>
    MapCoordinates? Location { get; }

    /// <summary>
    /// Gets the optional identifier (usually a username, account short name, or external reference)
    /// of the person or system that last reported the equipment's location.
    /// </summary>
    string? LocatedBy { get; }

    /// <summary>
    /// Gets the optional timestamp when the equipment's location was last recorded.
    /// </summary>
    DateTimeOffset? LocatedAt { get; }

    /// <summary>
    /// Gets the timestamp when this equipment record was first created in the system.
    /// </summary>
    DateTimeOffset CreatedOn { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Accounts.IAccount{TID}"/> that owns or is responsible for this equipment.
    /// </summary>
    TID? AccountId { get; }
}