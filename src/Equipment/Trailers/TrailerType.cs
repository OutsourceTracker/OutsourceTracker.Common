namespace OutsourceTracker.Equipment.Trailers;

/// <summary>
/// Defines the possible types of trailers used in the OutsourceTracker application.
/// This enumeration categorizes different trailer configurations for equipment tracking,
/// supporting inventory management, dispatching, and reporting based on trailer capabilities and requirements.
/// </summary>
public enum TrailerType
{
    /// <summary>
    /// The type of the trailer is unknown or has not been specified.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// A standard enclosed dry van trailer, suitable for general freight transportation.
    /// </summary>
    Van = 1,

    /// <summary>
    /// A refrigerated (reefer) trailer, designed for temperature-controlled shipments such as perishable goods.
    /// </summary>
    Reefer = 2,

    /// <summary>
    /// An open-deck flatbed trailer, ideal for oversized loads or items requiring side or top loading.
    /// </summary>
    Flatbed = 3,

    /// <summary>
    /// A Conestoga trailer, featuring a sliding tarp system for weather protection on a flatbed-style base.
    /// </summary>
    Conestoga = 4
}