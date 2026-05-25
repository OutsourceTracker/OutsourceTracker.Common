using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OutsourceTracker.Services.ModelService;
using System.Linq.Expressions;

namespace OutsourceTracker.Tools;

public sealed class ModelUpdater<TModel> : IModelUpdateBuilder<TModel> where TModel : class
{
    private IServiceProvider Services { get; set; }

    private TModel? ReferenceModel { get; set; }

    private Dictionary<string, object?> ValueUpdate { get; set; }

    private ILogger Logger { get; set; }

    private ModelUpdater(IServiceProvider services, TModel? model)
    {
        Services = services;
        ReferenceModel = model;
        Logger = services.GetService<ILogger<ModelUpdater<TModel>>>()!;
        ValueUpdate = [];
    }

    public IModelUpdateBuilder<TModel> Clear<TProperty>(Expression<Func<TModel, TProperty>> selector)
    {
        string propertyName = ExpressionHelper.GetPropertyName(selector);
        ValueUpdate.Remove(propertyName);
        return this;
    }

    public IModelUpdateBuilder<TModel> Set<TProperty>(Expression<Func<TModel, TProperty>> selector, TProperty? value = default)
    {
        string propertyName = ExpressionHelper.GetPropertyName(selector);

        if (ReferenceModel != null)
        {
            var func = selector.Compile();
            TProperty oldVal = func(ReferenceModel);

            if (HashCode.Equals(oldVal, value))
            {
                Logger.LogDebug("Skipping property update on {PropertyName}, values are the same", propertyName);
                return this;
            }
        }

        ValueUpdate[propertyName] = value;
        Logger.LogDebug("Tracking property update for {PropertyName}", propertyName);
        return this;
    }

    public IModelUpdateBuilder<TModel> SetService<TProperty>(Expression<Func<TModel, TProperty>> selector, bool required = false)
    {
        string propertyName = ExpressionHelper.GetPropertyName(selector);
#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
        TProperty? value = !required ? Services.GetService<TProperty>() : Services.GetRequiredService<TProperty>();
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
        ValueUpdate[propertyName] = value;
        return this;
    }

    public IReadOnlyDictionary<string, object?> Build() => ValueUpdate.AsReadOnly();

    public TModel Apply(TModel? model = null)
    {
        model ??= ReferenceModel;
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        model.ApplyObjectToModel(ValueUpdate);
        return model;
    }

    public static IModelUpdateBuilder<TModel> Update(IServiceProvider services, TModel? model = null) => new ModelUpdater<TModel>(services, model);
}
