using OutsourceTracker.Services.ModelService;

namespace OutsourceTracker.BusinessUnit.Divisions;

public interface IOrganizationalUnit<TID> : IServiceModel<TID> where TID : struct
{
    string ShortCode { get; set; }

    string Name { get; set; }

    string Description { get; set; }

    int TotalAccounts { get; set; }
}
