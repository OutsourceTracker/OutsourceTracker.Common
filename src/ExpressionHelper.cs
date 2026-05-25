using System.Linq.Expressions;
using System.Reflection;

namespace OutsourceTracker;

/// <summary>
/// Helper class for building dynamic LINQ expressions for EF Core queries.
/// </summary>
public static class ExpressionHelper
{
    /// <summary>
    /// Builds an equality predicate (Property == value).
    /// </summary>
    public static Expression<Func<T, bool>> BuildEqual<T>(string propertyName, object value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetPropertyExpression<T>(parameter, propertyName);
        var constant = Expression.Constant(value, property.Type);

        var body = Expression.Equal(property, constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Builds a GreaterThanOrEqual predicate (Property >= value).
    /// </summary>
    public static Expression<Func<T, bool>> BuildGreaterThanOrEqual<T>(string propertyName, object value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetPropertyExpression<T>(parameter, propertyName);
        var constantValue = ConvertValue(value, property.Type);
        var constant = Expression.Constant(constantValue, property.Type);

        var body = Expression.GreaterThanOrEqual(property, constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Builds a LessThanOrEqual predicate (Property &lt;= value).
    /// </summary>
    public static Expression<Func<T, bool>> BuildLessThanOrEqual<T>(string propertyName, object value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetPropertyExpression<T>(parameter, propertyName);
        var constantValue = ConvertValue(value, property.Type);
        var constant = Expression.Constant(constantValue, property.Type);

        var body = Expression.LessThanOrEqual(property, constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Builds a StartsWith predicate for string properties (or Guid.ToString()).
    /// </summary>
    public static Expression<Func<T, bool>> BuildStartsWith<T>(string propertyName, string value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetPropertyExpression<T>(parameter, propertyName);

        // Handle Guid by calling ToString()
        Expression stringExpression = property.Type == typeof(Guid)
            ? Expression.Call(property, typeof(Guid).GetMethod(nameof(Guid.ToString), Type.EmptyTypes)!)
            : property;

        var startsWithMethod = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) })!;
        var constant = Expression.Constant(value, typeof(string));

        var body = Expression.Call(stringExpression, startsWithMethod, constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Builds a Contains predicate for string properties (or Guid.ToString()).
    /// </summary>
    public static Expression<Func<T, bool>> BuildContains<T>(string propertyName, string value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetPropertyExpression<T>(parameter, propertyName);
        Expression stringExpression = property.Type == typeof(Guid)
            ? Expression.Call(property, typeof(Guid).GetMethod(nameof(Guid.ToString), Type.EmptyTypes)!)
            : property;
        var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;
        var constant = Expression.Constant(value, typeof(string));
        var body = Expression.Call(stringExpression, containsMethod, constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Builds a EndsWith predicate for string properties (or Guid.ToString()).
    /// </summary>
    public static Expression<Func<T, bool>> BuildEndsWith<T>(string propertyName, string value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetPropertyExpression<T>(parameter, propertyName);
        Expression stringExpression = property.Type == typeof(Guid)
            ? Expression.Call(property, typeof(Guid).GetMethod(nameof(Guid.ToString), Type.EmptyTypes)!)
            : property;
        var endsWithMethod = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) })!;
        var constant = Expression.Constant(value, typeof(string));
        var body = Expression.Call(stringExpression, endsWithMethod, constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private static MemberExpression GetPropertyExpression<T>(ParameterExpression parameter, string propertyName)
    {
        var propertyInfo = typeof(T).GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (propertyInfo == null)
            throw new ArgumentException($"Property '{propertyName}' not found on type {typeof(T).Name}.");

        return Expression.Property(parameter, propertyInfo);
    }

    private static object ConvertValue(object value, Type targetType)
    {
        if (value.GetType() == targetType)
            return value;

        if (targetType == typeof(DateTimeOffset) && value is DateTime dt)
            return new DateTimeOffset(dt);

        return Convert.ChangeType(value, targetType);
    }

    public static string GetPropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> selector)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        if (selector.Body is MemberExpression member)
        {
            return member.Member.Name;
        }

        throw new ArgumentException("Selector must be a simple property access expression (e.g. x => x.Property)", nameof(selector));
    }
}
