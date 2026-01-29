namespace OutsourceTracker.Services;

public interface IQueryableModelService<TModel> where TModel : class
{
    IQueryable<TModel> QueryModel();
}
