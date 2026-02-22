using OutsourceTracker.Geolocation;
using OutsourceTracker.Services.ModelService;

namespace OutsourceTracker.Equipment;

/// <summary>
/// Defines an equipment interface that extends the base data model with properties
/// for tracking physical or rented equipment in the OutsourceTracker application.
/// This interface supports inventory management, location tracking, and accountability
/// by linking equipment to accounts and recording state changes over time.
/// </summary>
/// <typeparam name="TID">The type of the identifier inherited from <see cref="IServiceModel{TID}"/> (e.g., int, Guid).</typeparam>
public interface IEquipment<TID> : IServiceModel<TID>, ITrackableEntity<TID> where TID : struct
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
    /// Gets the identifier of the <see cref="Accounts.IAccount{TID}"/> that owns or is responsible for this equipment.
    /// </summary>
    TID? AccountId { get; }
}