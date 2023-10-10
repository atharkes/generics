using Generics.Specifications.Interfaces;
using QueryInterceptor.Core;

namespace Generics.Specifications.Enumerable {
    public static class EnumerableExtensions {
        public static IEnumerable<T> Apply<T>(this IEnumerable<T> enumerable, ISpecification<T> specification)
            => specification.Apply(enumerable.AsQueryable()).InterceptWith(EnumerableExpressionModifier.Default);

        public static IEnumerable<TResult> Apply<T, TResult>(this IEnumerable<T> enumerable, ISpecification<T, TResult> specification)
            => specification.Apply(enumerable.AsQueryable()).InterceptWith(EnumerableExpressionModifier.Default);
    }
}
