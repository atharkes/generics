using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Generics.Specifications.Extensions {
    public interface IIncludableQueryable<out T, out TProperty> : IQueryable<T> { }

    public interface IIncludableEnumerable<out T, out TProperty> : IEnumerable<T> { }

    public static class QueryableIncludeExtensions {
        internal static readonly MethodInfo IncludeMethodInfo = typeof(QueryableIncludeExtensions).GetTypeInfo().GetDeclaredMethods(nameof(Include)).Single(IsIncludeMethod);
        internal static readonly MethodInfo ThenIncludeAfterEnumerableMethodInfo = typeof(QueryableIncludeExtensions).GetTypeInfo().GetDeclaredMethods(nameof(ThenInclude)).Single(IsThenIncludeAfterEnumerableMethod);
        internal static readonly MethodInfo ThenIncludeAfterReferenceMethodInfo = typeof(QueryableIncludeExtensions).GetTypeInfo().GetDeclaredMethods(nameof(ThenInclude)).Single(IsThenIncludeAfterReferenceMethod);

        public static bool IsIncludeMethod(MethodInfo methodInfo)
            => methodInfo.DeclaringType == typeof(QueryableIncludeExtensions)
            && methodInfo.Name == nameof(Include)
            && methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>);

        public static bool IsThenIncludeAfterEnumerableMethod(MethodInfo methodInfo)
            => methodInfo.DeclaringType == typeof(QueryableIncludeExtensions)
            && methodInfo.Name == nameof(ThenInclude)
            && methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>)
            && methodInfo.GetParameters()[0].ParameterType.GenericTypeArguments[1].IsGenericType
            && methodInfo.GetParameters()[0].ParameterType.GenericTypeArguments[1].GetGenericTypeDefinition() == typeof(IEnumerable<>);

        public static bool IsThenIncludeAfterReferenceMethod(MethodInfo methodInfo)
            => methodInfo.DeclaringType == typeof(QueryableIncludeExtensions)
            && methodInfo.Name == nameof(ThenInclude)
            && methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>)
            && !IsThenIncludeAfterEnumerableMethod(methodInfo);

        public static IIncludableQueryable<T, TProperty> Include<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> navigationExpression) {
            var expression = Expression.Call(null, IncludeMethodInfo.MakeGenericMethod(typeof(T), typeof(TProperty)), new[] { source.Expression, Expression.Quote(navigationExpression) });
            return new IncludableQueryable<T, TProperty>(source.Provider.CreateQuery<T>(expression));
        }

        public static IIncludableEnumerable<T, TProperty> Include<T, TProperty>(this IEnumerable<T> source, Func<T, TProperty> navigationFunction) {
            _ = source.Select(navigationFunction);

            return new IncludableEnumerable<T, TProperty>(source);
        }

        public static IIncludableQueryable<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(this IIncludableQueryable<T, IEnumerable<TPreviousProperty>> source, Expression<Func<TPreviousProperty, TProperty>> navigationExpression) {
            var expression = Expression.Call(null, ThenIncludeAfterEnumerableMethodInfo.MakeGenericMethod(typeof(T), typeof(TPreviousProperty), typeof(TProperty)), new[] { source.Expression, Expression.Quote(navigationExpression) });
            return new IncludableQueryable<T, TProperty>(source.Provider.CreateQuery<T>(expression));
        }

        public static IIncludableEnumerable<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(this IIncludableEnumerable<T, IEnumerable<TPreviousProperty>> source, Func<TPreviousProperty, TProperty> navigationFunction) {
            return new IncludableEnumerable<T, TProperty>(source);
        }

        public static IIncludableQueryable<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(this IIncludableQueryable<T, TPreviousProperty> source, Expression<Func<TPreviousProperty, TProperty>> navigationExpression) {
            var expression = Expression.Call(null, ThenIncludeAfterReferenceMethodInfo.MakeGenericMethod(typeof(T), typeof(TPreviousProperty), typeof(TProperty)), new[] { source.Expression, Expression.Quote(navigationExpression) });
            return new IncludableQueryable<T, TProperty>(source.Provider.CreateQuery<T>(expression));
        }

        public static IIncludableEnumerable<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(this IIncludableEnumerable<T, TPreviousProperty> source, Func<TPreviousProperty, TProperty> navigationFunction) {
            return new IncludableEnumerable<T, TProperty>(source);
        }

        private sealed class IncludableQueryable<T, TProperty> : IIncludableQueryable<T, TProperty>, IAsyncEnumerable<T> {
            private readonly IQueryable<T> _queryable;

            public IncludableQueryable(IQueryable<T> queryable)
                => _queryable = queryable;

            public Expression Expression => _queryable.Expression;
            public Type ElementType => _queryable.ElementType;
            public IQueryProvider Provider => _queryable.Provider;
            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => ((IAsyncEnumerable<T>)_queryable).GetAsyncEnumerator(cancellationToken);
            public IEnumerator<T> GetEnumerator() => _queryable.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed class IncludableEnumerable<T, TProperty> : IIncludableEnumerable<T, TProperty> {
            private readonly IEnumerable<T> _enumerable;

            public IncludableEnumerable(IEnumerable<T> enumerable)
                => _enumerable = enumerable;

            public IEnumerator<T> GetEnumerator() => _enumerable.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
