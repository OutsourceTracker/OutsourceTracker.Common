using OutsourceTracker.Services;

namespace OutsourceTracker.Accounts;

public interface IAccount<TID> : IModel<TID>
{
    string Name { get; set; }

    string Address { get; set; }

    string LocationCode { get; set; }

    Guid DivisionId { get; set; }
}
