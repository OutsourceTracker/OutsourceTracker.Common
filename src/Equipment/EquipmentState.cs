namespace OutsourceTracker.Equipment;

/// <summary>
/// Defines the possible operational states of equipment in the OutsourceTracker application.
/// This enumeration is used to track and categorize the status of equipment items,
/// facilitating management, reporting, and workflow decisions based on availability and condition.
/// </summary>
public enum EquipmentState
{
    /// <summary>
    /// The state of the equipment is unknown or has not been determined.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The equipment is available for use or assignment.
    /// </summary>
    Available = 1,

    /// <summary>
    /// The equipment has been dispatched or assigned to a task/location.
    /// </summary>
    Dispatched = 2,

    /// <summary>
    /// The equipment is mechanically down or in need of repair/maintenance.
    /// </summary>
    MechanicallyDown = 3
}