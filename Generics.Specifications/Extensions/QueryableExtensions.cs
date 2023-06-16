using System.Linq.Expressions;

namespace Generics.Specifications.Extensions {
    public static class QueryableExtensions {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool descending)
            => descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
    }
}
