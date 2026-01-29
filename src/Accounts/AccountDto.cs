namespace OutsourceTracker.Accounts;

public class AccountDto
{
    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? LocationCode { get; set; }

    public Guid? DivisionId { get; set; }
}
