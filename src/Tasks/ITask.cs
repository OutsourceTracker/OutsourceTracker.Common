using OutsourceTracker.Services.ModelService;

namespace OutsourceTracker.Tasks;

public interface ITask<TID> : IServiceModel<TID> where TID : struct
{
    string AssignedTo { get; }

    TaskType Type { get; }

    string JsonData { get; }

    DateTimeOffset? CompletedOn { get; }
}
