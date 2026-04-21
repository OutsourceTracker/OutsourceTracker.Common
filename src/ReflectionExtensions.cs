using System.Linq.Expressions;
using System.Reflection;

namespace OutsourceTracker;

public static class ReflectionExtensions
{
    private static readonly Dictionary<string, Func<object?, object?>> ParameterCache = new(StringComparer.Ordinal);

    public static TValue? PropertyOrField<TValue, T>(this T? item, string memberName, bool allowPrivate = false, bool isStatic = false) => (TValue?)PropertyOrField((object?)item, memberName, allowPrivate, isStatic);

    public static object? PropertyOrField<T>(this T? item, string memberName, bool allowPrivate = false, bool isStatic = false)
    {
        Type type = typeof(T);
        if (item == null)
        {
            isStatic = true;
        }

        string cacheKey = $"{type.FullName}{(isStatic ? ':' : '.')}{memberName}";

        if (ParameterCache.TryGetValue(cacheKey, out var cachedFunc))
        {
            return cachedFunc(item);
        }

        var targetParam = Expression.Parameter(typeof(object), "target");
        Expression? instanceExpr = isStatic ? null : Expression.Convert(targetParam, type);

        MemberInfo? memberInfo = GetMember(type, memberName, allowPrivate, isStatic);

        if (memberInfo == null)
        {
            throw new ArgumentException($"No property or field named '{memberName}' found on type '{type.FullName}'.");
        }

        Expression memberAccess = memberInfo switch
        {
            FieldInfo field => isStatic
                ? Expression.Field(null, field)
                : Expression.Field(instanceExpr!, field),

            PropertyInfo property => isStatic
                ? Expression.Property(null, property)
                : Expression.Property(instanceExpr!, property),

            _ => throw new InvalidOperationException("Unsupported member type.")
        };

        Expression body = Expression.Convert(memberAccess, typeof(object));
        var lambda = Expression.Lambda<Func<object?, object?>>(body, targetParam);
        var compiled = lambda.Compile();

        lock (ParameterCache)
        {
            if (!ParameterCache.ContainsKey(cacheKey))
            {
                ParameterCache[cacheKey] = compiled;
            }
        }

        return compiled(item);
    }

    private static MemberInfo? GetMember(Type type, string name, bool allowPrivate, bool isStatic)
    {
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic;

        if (isStatic)
            flags |= BindingFlags.Static;
        else
            flags |= BindingFlags.Instance;

        if (!allowPrivate)
            flags &= ~BindingFlags.NonPublic;

        var property = type.GetProperty(name, flags);
        if (property != null)
            return property;

        return type.GetField(name, flags);
    }
}