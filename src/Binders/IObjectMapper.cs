namespace OutsourceTracker.Binders;

public interface IObjectMapper<TObject1, TObject2>
{
    TObject2 Update(TObject1 source, TObject2 destination);
}
