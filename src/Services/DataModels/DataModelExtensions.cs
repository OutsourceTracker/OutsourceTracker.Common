namespace OutsourceTracker.Services.DataModels;

public static class DataModelExtensions
{
    public static TResult? ToResult<TResult>(this ModelResult result)
    {
        if (result.Success)
        {
            if (result.Data is TResult r)
            {
                return r;
            }
        }

        return default;
    }
}
