using System.Linq.Expressions;

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
}
