namespace OutsourceTracker.Billing;

public interface IPayable
{
    Guid Id { get; set; }

    string Name { get; set; }

    decimal Value { get; set; }
}
