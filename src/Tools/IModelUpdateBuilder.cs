using System.Linq.Expressions;

namespace OutsourceTracker.Tools;

public interface IModelUpdateBuilder<TModel> where TModel : class
{
    IModelUpdateBuilder<TModel> Clear<TProperty>(Expression<Func<TModel, TProperty>> selector);

    IModelUpdateBuilder<TModel> Set<TProperty>(Expression<Func<TModel, TProperty>> selector, TProperty? value = default);

    IModelUpdateBuilder<TModel> SetService<TProperty>(Expression<Func<TModel, TProperty>> selector, bool required = false);

    IReadOnlyDictionary<string, object?> Build();

    TModel Apply(TModel? model = null);
}
