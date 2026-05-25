using OutsourceTracker.Services.ModelService;
using System.ComponentModel.DataAnnotations;

namespace OutsourceTracker.BusinessUnit.Accounts;

public interface IOrganizationAccount<TID> : IServiceModel<TID> where TID : struct
{
    string ShortCode { get; set; }

    string Name { get; set; }

    
    string? CostCenter { get; set; }

    string? GroupEmail { get; set; }

    string? Address { get; set; }

    TID OUID { get; set; }
}
