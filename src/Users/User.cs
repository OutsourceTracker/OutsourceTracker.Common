using OutsourceTracker.Services;

namespace OutsourceTracker.Users;

public class User<TID> : Model<TID>, IUser<TID>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string WorkdayId { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public override string ToString() => $"{FullName}:{WorkdayId}";

    public override int GetHashCode() => HashCode.Combine(Id, WorkdayId);
}
