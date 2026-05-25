namespace OutsourceTracker.Tools;

public static class ModelExtensions
{
    public static IModelUpdateBuilder<TModel> Update<TModel>(this TModel? model, IServiceProvider services) where TModel : class => ModelUpdater<TModel>.Update(services, model);
}
