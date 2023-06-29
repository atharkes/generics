using System.Linq.Expressions;

namespace Generics.Specifications.Extensions {
    public static class OrderedQueryableExtensions {
        public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool descending)
            => descending ? source.ThenByDescending(keySelector) : source.ThenBy(keySelector);
    }
}
