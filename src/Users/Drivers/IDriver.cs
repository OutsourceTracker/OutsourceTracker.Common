namespace OutsourceTracker.Users.Drivers;

public interface IDriver<TID> : IUser<TID>
{
    string AlphaCode { get; set; }

    Guid AccountId { get; set; }
}
