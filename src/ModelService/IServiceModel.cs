namespace OutsourceTracker.ModelService;

public interface IServiceModel<TID>
{
    TID Id { get; set; }
}
