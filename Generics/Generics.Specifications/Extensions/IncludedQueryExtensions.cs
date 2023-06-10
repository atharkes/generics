using Generics.Specifications.Interfaces;
using Generics.Specifications.Queries;
using System.Linq.Expressions;

namespace Generics.Specifications.Extensions {
    public static class IncludedQueryExtensions {
        public static IIncludedQuery<T, TNextProperty> ThenInclude<T, TProperty, TNextProperty>(
            this IIncludedQuery<T, IEnumerable<TProperty>> query,
            Expression<Func<TProperty, TNextProperty>> selector)
            => new ThenIncludeAfterEnumerableQuery<T, TProperty, TNextProperty>(query, selector);

        public static IIncludedQuery<T, TNextProperty> ThenInclude<T, TProperty, TNextProperty>(
            this IIncludedQuery<T, TProperty> query,
            Expression<Func<TProperty, TNextProperty>> selector)
            => new ThenIncludeAfterReferenceQuery<T, TProperty, TNextProperty>(query, selector);

        public static IIncludedQuery<TBase, T, TNextProperty> ThenInclude<TBase, T, TProperty, TNextProperty>(
            this IIncludedQuery<TBase, T, IEnumerable<TProperty>> query,
            Expression<Func<TProperty, TNextProperty>> selector)
            => new ThenIncludeAfterEnumerableQuery<TBase, T, TProperty, TNextProperty>(query, selector);

        public static IIncludedQuery<TBase, T, TNextProperty> ThenInclude<TBase, T, TProperty, TNextProperty>(
            this IIncludedQuery<TBase, T, TProperty> query,
            Expression<Func<TProperty, TNextProperty>> selector)
            => new ThenIncludeAfterReferenceQuery<TBase, T, TProperty, TNextProperty>(query, selector);
    }
}
