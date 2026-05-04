using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Reflection;

namespace OutsourceTracker;

public static class UtilityExtensions
{
    public static string? MemberName<TEntity, TValue>(this TEntity? ent, Expression<Func<TEntity, TValue>> expr) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(expr);

        if (expr.Body is MemberExpression mexpr)
        {
            return mexpr.Member.Name;
        }

        return null;
    }

    #region Queryable Extensions

    public static IQueryable<T> ApplySearchParameters<T, TParameters>(this IQueryable<T> query, TParameters parameters, ILogger? logger = null) where T : class
    {
        if (parameters is null)
            return query;

        var paramType = typeof(TParameters);
        var props = paramType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props)
        {
            object? value;
            try
            {
                if (prop.GetIndexParameters().Length > 0)
                    continue;

                value = prop.GetValue(parameters);
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Failed to read search property '{Property}' on {ParamType}",
                    prop.Name, paramType.Name);
                continue;
            }

            if (value == null || (value is string s && string.IsNullOrWhiteSpace(s)))
                continue;

            var propName = prop.Name;

            try
            {
                var modelProp = typeof(T).GetProperty(propName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (modelProp != null)
                {
                    query = query.Where(ExpressionHelper.BuildEqual<T>(modelProp.Name, value));
                    continue;
                }

                if (propName.EndsWith("After", StringComparison.OrdinalIgnoreCase))
                {
                    var baseName = propName[..^5];
                    query = query.Where(ExpressionHelper.BuildGreaterThanOrEqual<T>(baseName, value));
                }
                else if (propName.EndsWith("Before", StringComparison.OrdinalIgnoreCase))
                {
                    var baseName = propName[..^6];
                    query = query.Where(ExpressionHelper.BuildLessThanOrEqual<T>(baseName, value));
                }
                else if (propName.EndsWith("Contains", StringComparison.OrdinalIgnoreCase))
                {
                    var baseName = propName[..^8];
                    if (value is string strValue && !string.IsNullOrEmpty(strValue))
                    {
                        query = query.Where(ExpressionHelper.BuildContains<T>(baseName, strValue));
                    }
                }
                else if (propName.EndsWith("EndsWith", StringComparison.OrdinalIgnoreCase))
                {
                    var baseName = propName[..^8];
                    if (value is string strValue && !string.IsNullOrEmpty(strValue))
                    {
                        query = query.Where(ExpressionHelper.BuildEndsWith<T>(baseName, strValue));
                    }
                }
                else if (value is string strValue && !string.IsNullOrEmpty(strValue))
                {
                    query = query.Where(ExpressionHelper.BuildStartsWith<T>(propName, strValue));
                }
                else
                {
                    logger?.LogDebug("No matching property found for search parameter '{ParamName}' on type '{TypeName}'",
                        propName, typeof(T).Name);
                }
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Failed to apply search filter for property '{Property}' on model '{ModelType}'",
                    propName, typeof(T).Name);
            }
        }

        return query;
    }

    #endregion
}
