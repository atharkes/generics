using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Generics.Specifications.Extensions {
    public interface IIncludableQueryable<out T, out TProperty> : IQueryable<T> { }

    public static class QueryableIncludeExtensions {
        internal static readonly MethodInfo IncludeMethodInfo = typeof(QueryableIncludeExtensions).GetTypeInfo().GetDeclaredMethods(nameof(Include)).Single(IsIncludeMethod);
        internal static readonly MethodInfo ThenIncludeAfterEnumerableMethodInfo = typeof(QueryableIncludeExtensions).GetTypeInfo().GetDeclaredMethods(nameof(ThenInclude)).Single(IsThenIncludeAfterEnumerableMethod);
        internal static readonly MethodInfo ThenIncludeAfterReferenceMethodInfo = typeof(QueryableIncludeExtensions).GetTypeInfo().GetDeclaredMethods(nameof(ThenInclude)).Single(IsThenIncludeAfterReferenceMethod);

        public static bool IsIncludeMethod(MethodInfo methodInfo)
            => methodInfo.DeclaringType == typeof(QueryableIncludeExtensions)
            && methodInfo.Name == nameof(Include);

        public static bool IsThenIncludeAfterEnumerableMethod(MethodInfo methodInfo) {
            if (methodInfo.DeclaringType != typeof(QueryableIncludeExtensions)) return false;
            if (methodInfo.Name != nameof(ThenInclude)) return false;

            var typeInfo = methodInfo.GetParameters()[0].ParameterType.GenericTypeArguments[1];
            return typeInfo.IsGenericType
                && typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static bool IsThenIncludeAfterReferenceMethod(MethodInfo methodInfo)
            => methodInfo.DeclaringType == typeof(QueryableIncludeExtensions)
            && methodInfo.Name == nameof(ThenInclude)
            && !IsThenIncludeAfterEnumerableMethod(methodInfo);

        public static IIncludableQueryable<T, TProperty> Include<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> navigationExpression) {
            var expression = Expression.Call(null, IncludeMethodInfo.MakeGenericMethod(typeof(T), typeof(TProperty)), new[] { source.Expression, Expression.Quote(navigationExpression) });
            return new IncludableQueryable<T, TProperty>(source.Provider.CreateQuery<T>(expression));
        }

        public static IIncludableQueryable<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(this IIncludableQueryable<T, IEnumerable<TPreviousProperty>> source, Expression<Func<TPreviousProperty, TProperty>> navigationExpression) {
            var expression = Expression.Call(null, ThenIncludeAfterEnumerableMethodInfo.MakeGenericMethod(typeof(T), typeof(TPreviousProperty), typeof(TProperty)), new[] { source.Expression, Expression.Quote(navigationExpression) });
            return new IncludableQueryable<T, TProperty>(source.Provider.CreateQuery<T>(expression));
        }

        public static IIncludableQueryable<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(this IIncludableQueryable<T, TPreviousProperty> source, Expression<Func<TPreviousProperty, TProperty>> navigationExpression) {
            var expression = Expression.Call(null, ThenIncludeAfterReferenceMethodInfo.MakeGenericMethod(typeof(T), typeof(TPreviousProperty), typeof(TProperty)), new[] { source.Expression, Expression.Quote(navigationExpression) });
            return new IncludableQueryable<T, TProperty>(source.Provider.CreateQuery<T>(expression));
        }

        sealed class IncludableQueryable<T, TProperty> : IIncludableQueryable<T, TProperty>, IAsyncEnumerable<T> {
            readonly IQueryable<T> _queryable;

            public IncludableQueryable(IQueryable<T> queryable) {
                _queryable = queryable;
            }

            public Expression Expression => _queryable.Expression;
            public Type ElementType => _queryable.ElementType;
            public IQueryProvider Provider => _queryable.Provider;
            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => ((IAsyncEnumerable<T>)_queryable).GetAsyncEnumerator(cancellationToken);
            public IEnumerator<T> GetEnumerator() => _queryable.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
