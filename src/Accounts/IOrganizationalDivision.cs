using OutsourceTracker.Services;

namespace OutsourceTracker.Accounts;

public interface IOrganizationalDivision<TID> : IModel<TID>
{
    string Name { get; set; }

    string ShortCode { get; set; }

    string? Description { get; set; }
}
