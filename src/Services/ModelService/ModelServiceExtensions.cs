using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace OutsourceTracker.Services.ModelService;

public static class ModelServiceExtensions
{
    public static ImmutableArray<string> KnownQueries { get; } = ImmutableArray.Create(
        "GreaterThanOrEqual", "LessThanOrEqual",
        "StartsWith", "EndsWith",
        "GreaterThan", "LessThan",
        "Contains", "Equals",
        "Before", "After",
        "From", "To",
        "In");

    public static IQueryable<TModel> ApplyObjectFilter<TModel>(this IQueryable<TModel> query, object? searchParameters)
    {
        if (searchParameters == null)
        {
            return query;
        }

        var param = Expression.Parameter(typeof(TModel), "m");

        IEnumerable<KeyValuePair<string, object?>>? keyValuePairs = null;

        if (searchParameters is IDictionary<string, object> dict)
        {
            keyValuePairs = dict.Select(kvp => new KeyValuePair<string, object?>(kvp.Key, kvp.Value));
        }
        else
        {
            var properties = searchParameters.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            keyValuePairs = properties.Select(pi => new KeyValuePair<string, object?>(pi.Name, pi.GetValue(searchParameters)));
        }

        if (keyValuePairs == null)
        {
            return query;
        }

        foreach (var kvp in keyValuePairs)
        {
            var value = kvp.Value;
            if (value == null)
            {
                continue;
            }

            string propName = kvp.Key;
            string modelProp = propName;
            string term = string.Empty;

            foreach (var knownTerm in KnownQueries)
            {
                if (propName.EndsWith(knownTerm, StringComparison.OrdinalIgnoreCase))
                {
                    term = knownTerm.ToLowerInvariant();
                    modelProp = propName.Substring(0, propName.Length - knownTerm.Length);
                    break;
                }

                if (propName.StartsWith(knownTerm, StringComparison.OrdinalIgnoreCase))
                {
                    term = knownTerm.ToLowerInvariant();
                    modelProp = propName.Substring(knownTerm.Length);
                    break;
                }
            }

            var modelPropInfo = typeof(TModel).GetProperties().FirstOrDefault(p => p.Name.Equals(modelProp, StringComparison.OrdinalIgnoreCase));
            if (modelPropInfo == null)
            {
                continue;
            }

            var propType = modelPropInfo.PropertyType;
            object? convertedValue;
            if (!TryConvert(value, propType, out convertedValue))
            {
                continue;
            }

            var member = Expression.Property(param, modelPropInfo);
            Expression? binExpr = null;

            switch (term)
            {
                case "":
                case "equals":
                    binExpr = Expression.Equal(member, Expression.Constant(convertedValue, propType));
                    break;
                case "before":
                case "lessthan":
                    binExpr = Expression.LessThan(member, Expression.Constant(convertedValue, propType));
                    break;
                case "after":
                case "greaterthan":
                    binExpr = Expression.GreaterThan(member, Expression.Constant(convertedValue, propType));
                    break;
                case "lessthanorequal":
                case "to":
                    binExpr = Expression.LessThanOrEqual(member, Expression.Constant(convertedValue, propType));
                    break;
                case "greaterthanorequal":
                case "from":
                    binExpr = Expression.GreaterThanOrEqual(member, Expression.Constant(convertedValue, propType));
                    break;
                case "contains":
                    if (propType == typeof(string))
                    {
                        binExpr = Expression.Call(member, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, Expression.Constant(convertedValue, propType));
                    }
                    break;
                case "startswith":
                    if (propType == typeof(string))
                    {
                        binExpr = Expression.Call(member, typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!, Expression.Constant(convertedValue, propType));
                    }
                    break;
                case "endswith":
                    if (propType == typeof(string))
                    {
                        binExpr = Expression.Call(member, typeof(string).GetMethod("EndsWith", new[] { typeof(string) })!, Expression.Constant(convertedValue, propType));
                    }
                    break;
                case "in":
                    var enumerableType = typeof(IEnumerable<>).MakeGenericType(propType);
                    if (TryConvert(value, enumerableType, out var convList))
                    {
                        var containsMethod = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                            .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                            .MakeGenericMethod(propType);
                        binExpr = Expression.Call(null, containsMethod, Expression.Constant(convList, enumerableType), member);
                    }
                    break;
            }

            if (binExpr != null)
            {
                var lambda = Expression.Lambda<Func<TModel, bool>>(binExpr, param);
                query = query.Where(lambda);
            }
        }

        return query;
    }

    public static bool ApplyObjectToModel<TModel>(this TModel model, object parameters)
    {
        if (model == null || parameters == null)
        {
            return false;
        }

        var modelType = typeof(TModel);
        bool changed = false;

        IEnumerable<KeyValuePair<string, object?>>? keyValuePairs = null;

        if (parameters is IDictionary<string, object> dict)
        {
            keyValuePairs = dict.Select(kvp => new KeyValuePair<string, object?>(kvp.Key, kvp.Value));
        }
        else
        {
            var paramType = parameters.GetType();
            var properties = paramType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            keyValuePairs = properties.Select(pi => new KeyValuePair<string, object?>(pi.Name, pi.GetValue(parameters)));
        }

        if (keyValuePairs == null)
        {
            return false;
        }

        foreach (var kvp in keyValuePairs)
        {
            var value = kvp.Value;
            if (value == null)
            {
                continue;
            }

            var modelProp = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name.Equals(kvp.Key, StringComparison.OrdinalIgnoreCase));
            if (modelProp == null || !modelProp.CanWrite)
            {
                continue;
            }

            var ignoreAttr = modelProp.GetCustomAttribute<IgnoreParameterAttribute>();
            if (ignoreAttr != null)
            {
                continue;
            }

            object? convertedValue;
            if (TryConvert(value, modelProp.PropertyType, out convertedValue))
            {
                modelProp.SetValue(model, convertedValue);
                changed = true;
            }
        }

        return changed;
    }

    private static bool TryConvert(object? input, Type targetType, out object? result)
    {
        result = null;
        if (input == null)
        {
            return false;
        }

        var inputType = input.GetType();
        if (inputType == targetType || targetType.IsAssignableFrom(inputType))
        {
            result = input;
            return true;
        }

        var fromConverter = TypeDescriptor.GetConverter(inputType);
        if (fromConverter.CanConvertTo(targetType))
        {
            try
            {
                result = fromConverter.ConvertTo(input, targetType);
                return true;
            }
            catch
            {
            }
        }

        var toConverter = TypeDescriptor.GetConverter(targetType);
        if (toConverter.CanConvertFrom(inputType))
        {
            try
            {
                result = toConverter.ConvertFrom(input);
                return true;
            }
            catch
            {
            }
        }

        try
        {
            if (targetType == typeof(DateTimeOffset) && input is DateTime dt)
            {
                result = new DateTimeOffset(dt, TimeSpan.Zero);
                return true;
            }
            else if (targetType == typeof(DateTime) && input is DateTimeOffset dto)
            {
                result = dto.UtcDateTime;
                return true;
            }
            else if (targetType == typeof(DateOnly) && input is DateTime dtOnly)
            {
                result = DateOnly.FromDateTime(dtOnly);
                return true;
            }
            else if (targetType == typeof(DateOnly) && input is DateTimeOffset dtoOnly)
            {
                result = DateOnly.FromDateTime(dtoOnly.UtcDateTime);
                return true;
            }
            else if (targetType == typeof(DateTime) && input is DateOnly dateOnly)
            {
                result = dateOnly.ToDateTime(TimeOnly.MinValue);
                return true;
            }
            else if (targetType == typeof(DateTimeOffset) && input is DateOnly dateOnlyOffset)
            {
                result = new DateTimeOffset(dateOnlyOffset.ToDateTime(TimeOnly.MinValue), TimeSpan.Zero);
                return true;
            }
        }
        catch
        {
        }

        // Handle collections (enhanced for query string scenarios)
        if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            var elementType = targetType.GetGenericArguments()[0];

            if (input is string strInput && strInput.Contains(',')) // Handle comma-separated query param like "1,2,3"
            {
                var items = strInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var listType = typeof(List<>).MakeGenericType(elementType);
                var list = (IList)Activator.CreateInstance(listType)!;
                bool allConverted = true;

                foreach (var item in items)
                {
                    if (TryConvert(item, elementType, out var convItem))
                    {
                        list.Add(convItem);
                    }
                    else
                    {
                        allConverted = false;
                        break;
                    }
                }

                if (allConverted)
                {
                    result = list;
                    return true;
                }
            }
            else if (input is IEnumerable enumerableInput) // Handle repeated query params like ?val=1&val=2 or direct arrays
            {
                var listType = typeof(List<>).MakeGenericType(elementType);
                var list = (IList)Activator.CreateInstance(listType)!;
                bool allConverted = true;

                foreach (var item in enumerableInput)
                {
                    if (TryConvert(item, elementType, out var convItem))
                    {
                        list.Add(convItem);
                    }
                    else
                    {
                        allConverted = false;
                        break;
                    }
                }

                if (allConverted)
                {
                    result = list;
                    return true;
                }
            }
        }
        else if (targetType.IsArray)
        {
            var elementType = targetType.GetElementType()!;
            if (input is IEnumerable enumerableInput)
            {
                var tempList = new List<object?>();
                bool allConverted = true;

                foreach (var item in enumerableInput)
                {
                    if (TryConvert(item, elementType, out var convItem))
                    {
                        tempList.Add(convItem);
                    }
                    else
                    {
                        allConverted = false;
                        break;
                    }
                }

                if (allConverted)
                {
                    var array = Array.CreateInstance(elementType, tempList.Count);
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        array.SetValue(tempList[i], i);
                    }
                    result = array;
                    return true;
                }
            }
        }

        try
        {
            result = Convert.ChangeType(input, targetType);
            return true;
        }
        catch
        {
            return false;
        }
    }
}