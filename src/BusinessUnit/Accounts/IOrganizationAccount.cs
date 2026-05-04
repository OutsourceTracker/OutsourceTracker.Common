using OutsourceTracker.Services.ModelService;

namespace OutsourceTracker.BusinessUnit.Accounts;

public interface IOrganizationAccount<TID> : IServiceModel<TID> where TID : struct
{
    string ShortCode { get; set; }

    string Name { get; set; }

    TID OUID { get; set; }
}
