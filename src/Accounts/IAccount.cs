using OutsourceTracker.Data;

namespace OutsourceTracker.Accounts;

/// <summary>
/// Defines an account interface that extends the base data model with name properties.
/// This interface is used for representing accounts in the OutsourceTracker application,
/// such as business accounts, providing short and full name details for identification and display purposes.
/// </summary>
/// <typeparam name="TID">The type of the identifier inherited from <see cref="IDataModel{TID}"/> (e.g., int, Guid).</typeparam>
public interface IAccount<TID> : IDataModel<TID> where TID : struct
{
    /// <summary>
    /// Gets the short name or alias of the account, suitable for concise displays or references.
    /// </summary>
    string ShortName { get; }

    /// <summary>
    /// Gets the full name of the account, providing complete identification details.
    /// </summary>
    string FullName { get; }
}