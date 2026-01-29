using OutsourceTracker.Services;

namespace OutsourceTracker.Users;

public interface IUser<TID> : IModel<TID>
{
    string FirstName { get; set; }

    string LastName { get; set; }

    string FullName { get; set; }

    string WorkdayId { get; set; }

    string EmailAddress { get; set; }

    string PhoneNumber { get; set; }
}
