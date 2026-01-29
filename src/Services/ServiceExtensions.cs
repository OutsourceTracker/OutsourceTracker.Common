using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace OutsourceTracker.Services;

public static class ServiceExtensions
{
    private static readonly ConcurrentDictionary<(Type ModelType, string PropertyName), Action<object, object>> PropertySetters = new();

    public static async Task<TModel> UpdateModel<TID, TModel>(this IModelService<TID, TModel> modelService, TID modelId, object dto, CancellationToken cancellationToken = default, bool skipNullValues = true) where TModel : IModel<TID>
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        TModel databaseObject = await modelService.UpdateModel(modelId, model =>
        {
            bool changed = false;
            Type modelType = typeof(TModel);
            Type dtoType = dto.GetType();


            var modelProperties = modelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && p.GetSetMethod(false) != null);

            foreach (var modelProp in modelProperties)
            {
                if (string.Equals(modelProp.Name, "Id", StringComparison.OrdinalIgnoreCase))
                    continue;

                var dtoProp = dtoType.GetProperty(modelProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (dtoProp == null || !dtoProp.CanRead)
                    continue;

                if (!modelProp.PropertyType.IsAssignableFrom(dtoProp.PropertyType))
                {
                    // Special handling for nullable types
                    var targetType = Nullable.GetUnderlyingType(modelProp.PropertyType) ?? modelProp.PropertyType;
                    var sourceType = Nullable.GetUnderlyingType(dtoProp.PropertyType) ?? dtoProp.PropertyType;

                    if (targetType != sourceType && !targetType.IsAssignableFrom(sourceType))
                        continue;
                }

                object? dtoValue = dtoProp.GetValue(dto);

                if (dtoValue == null && skipNullValues)
                {
                    continue;
                }

                object? currentValue = modelProp.GetValue(model);

                if (!object.Equals(currentValue, dtoValue))
                {
                    // Use cached fast setter
                    var setter = GetPropertySetter(modelType, modelProp.Name);
                    setter(model, dtoValue!);
                    changed = true;
                }
            }

            return changed;
        }, cancellationToken);

        return databaseObject;
    }

    public static async Task<TModel> AddModel<TID, TModel>(this IModelService<TID, TModel> modelService, object dto, CancellationToken cancellationToken = default) where TModel : IModel<TID>
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));
        TModel databaseObject = await modelService.AddModel(model =>
        {
            Type modelType = typeof(TModel);
            Type dtoType = dto.GetType();
            var modelProperties = modelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && p.GetSetMethod(false) != null);

            foreach (var modelProp in modelProperties)
            {
                if (string.Equals(modelProp.Name, "Id", StringComparison.OrdinalIgnoreCase))
                    continue;

                var dtoProp = dtoType.GetProperty(modelProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (dtoProp == null || !dtoProp.CanRead)
                    continue;

                if (!modelProp.PropertyType.IsAssignableFrom(dtoProp.PropertyType))
                {
                    // Special handling for nullable types
                    var targetType = Nullable.GetUnderlyingType(modelProp.PropertyType) ?? modelProp.PropertyType;
                    var sourceType = Nullable.GetUnderlyingType(dtoProp.PropertyType) ?? dtoProp.PropertyType;
                    if (targetType != sourceType && !targetType.IsAssignableFrom(sourceType))
                        continue;
                }
                object? dtoValue = dtoProp.GetValue(dto);
                if (dtoValue != null)
                {
                    // Use cached fast setter
                    var setter = GetPropertySetter(modelType, modelProp.Name);
                    setter(model, dtoValue);
                }
            }
        }, cancellationToken);
        return databaseObject;
    }

    // Builds and caches a fast property setter using Expression Trees
    private static Action<object, object> GetPropertySetter(Type modelType, string propertyName)
    {
        var cacheKey = (modelType, propertyName);

        return PropertySetters.GetOrAdd(cacheKey, key =>
        {
            var instanceParam = Expression.Parameter(typeof(object), "instance");
            var valueParam = Expression.Parameter(typeof(object), "value");

            var instance = Expression.Convert(instanceParam, modelType);
            var value = Expression.Convert(valueParam, key.ModelType.GetProperty(key.PropertyName)!.PropertyType);

            var property = Expression.Property(instance, key.PropertyName);
            var assign = Expression.Assign(property, value);

            var lambda = Expression.Lambda<Action<object, object>>(assign, instanceParam, valueParam);
            return lambda.Compile();
        });
    }
}
