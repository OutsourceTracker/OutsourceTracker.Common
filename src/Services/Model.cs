namespace OutsourceTracker.Services;

public abstract class Model<TID> : IModel<TID>
{
    public TID Id { get; set; } = default!;

    public override bool Equals(object? obj) => obj is Model<TID> other && EqualityComparer<TID>.Default.Equals(other.Id, Id);

    public override int GetHashCode() => HashCode.Combine(Id);
}
