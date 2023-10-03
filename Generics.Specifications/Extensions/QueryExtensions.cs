using Generics.Specifications.Interfaces;
using Generics.Specifications.Queries;
using System.Linq.Expressions;

namespace Generics.Specifications.Extensions {
    /// <summary> Extension methods for the <see cref="IQuery{T}"/> and <see cref="IQuery{TBase, TResult}"/> interfaces. </summary>
    public static class QueryExtensions {
        #region Include
        /// <summary> Specify related reference-properties to include in the <paramref name="query"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="query"/> operates on.</typeparam>
        /// <typeparam name="TProperty">The <see cref="Type"/> of the property to include</typeparam>
        /// <param name="query">The previous <see cref="IQuery{T}"/>.</param>
        /// <param name="selector">The <see cref="Expression{TDelegate}"/> that specifies the property to include.</param>
        /// <returns>A new <see cref="IIncludedQuery{T, TProperty}"/>.</returns>
        public static IIncludedQuery<T, TProperty> Include<T, TProperty>(
            this IQuery<T> query,
            Expression<Func<T, TProperty>> selector
         ) => new IncludeQuery<T, TProperty>(query, selector);

        public static IIncludedQuery<TBase, TResult, TProperty> Include<TBase, TResult, TProperty>(
            this IQuery<TBase, TResult> query,
            Expression<Func<TResult, TProperty>> selector
        ) => new IncludeQuery<TBase, TResult, TProperty>(query, selector);
        #endregion

        #region ThenInclude
        public static IIncludedQuery<T, TNextProperty> ThenInclude<T, TProperty, TNextProperty>(
            this IIncludedQuery<T, IEnumerable<TProperty>> query,
            Expression<Func<TProperty, TNextProperty>> selector
        ) => new ThenIncludeAfterEnumerableQuery<T, TProperty, TNextProperty>(query, selector);

        public static IIncludedQuery<T, TNextProperty> ThenInclude<T, TProperty, TNextProperty>(
            this IIncludedQuery<T, TProperty> query,
            Expression<Func<TProperty, TNextProperty>> selector
        ) => new ThenIncludeAfterReferenceQuery<T, TProperty, TNextProperty>(query, selector);

        public static IIncludedQuery<TBase, T, TNextProperty> ThenInclude<TBase, T, TProperty, TNextProperty>(
            this IIncludedQuery<TBase, T, IEnumerable<TProperty>> query,
            Expression<Func<TProperty, TNextProperty>> selector
        ) => new ThenIncludeAfterEnumerableQuery<TBase, T, TProperty, TNextProperty>(query, selector);

        public static IIncludedQuery<TBase, T, TNextProperty> ThenInclude<TBase, T, TProperty, TNextProperty>(
            this IIncludedQuery<TBase, T, TProperty> query,
            Expression<Func<TProperty, TNextProperty>> selector
        ) => new ThenIncludeAfterReferenceQuery<TBase, T, TProperty, TNextProperty>(query, selector);
        #endregion

        #region OrderBy
        public static IOrderedQuery<T> OrderBy<T, TProperty>(
            this IQuery<T> query,
            Expression<Func<T, TProperty>> selector,
            bool descending
        ) => new OrderByQuery<T, TProperty>(query, selector, descending);

        public static IOrderedQuery<TBase, T> OrderBy<TBase, T, TProperty>(
            this IQuery<TBase, T> query,
            Expression<Func<T, TProperty>> selector,
            bool descending
        ) => new OrderQuery<TBase, T, TProperty>(query, selector, descending);

        public static IOrderedQuery<T> OrderBy<T, TProperty>(
            this IQuery<T> query,
            Expression<Func<T, TProperty>> selector
        ) => query.OrderBy(selector, false);

        public static IOrderedQuery<TBase, T> OrderBy<TBase, T, TProperty>(
            this IQuery<TBase, T> query,
            Expression<Func<T, TProperty>> selector
        ) => query.OrderBy(selector, false);

        public static IOrderedQuery<T> OrderByDescending<T, TProperty>(
            this IQuery<T> query,
            Expression<Func<T, TProperty>> selector
        ) => query.OrderBy(selector, true);

        public static IOrderedQuery<TBase, T> OrderByDescending<TBase, T, TProperty>(
            this IQuery<TBase, T> query,
            Expression<Func<T, TProperty>> selector
        ) => query.OrderBy(selector, true);
        #endregion

        #region ThenBy
        public static IOrderedQuery<T> ThenBy<T, TNextProperty>(
            this IOrderedQuery<T> query,
            Expression<Func<T, TNextProperty>> selector,
            bool descending
        ) => new ThenByQuery<T, TNextProperty>(query, selector, descending);

        public static IOrderedQuery<TBase, T> ThenBy<TBase, T, TNextProperty>(
            this IOrderedQuery<TBase, T> query,
            Expression<Func<T, TNextProperty>> selector,
            bool descending
        ) => new ThenByQuery<TBase, T, TNextProperty>(query, selector, descending);

        public static IOrderedQuery<T> ThenBy<T, TProperty>(
            this IOrderedQuery<T> query,
            Expression<Func<T, TProperty>> selector
        ) => query.ThenBy(selector, false);

        public static IOrderedQuery<TBase, T> ThenBy<TBase, T, TProperty>(
            this IOrderedQuery<TBase, T> query,
            Expression<Func<T, TProperty>> selector
        ) => query.ThenBy(selector, false);

        public static IOrderedQuery<T> ThenByDescending<T, TProperty>(
            this IOrderedQuery<T> query,
            Expression<Func<T, TProperty>> selector
        ) => query.ThenBy(selector, true);

        public static IOrderedQuery<TBase, T> ThenByDescending<TBase, T, TProperty>(
            this IOrderedQuery<TBase, T> query,
            Expression<Func<T, TProperty>> selector
        ) => query.ThenBy(selector, true);
        #endregion

        #region Select
        public static IQuery<T, TProperty> Select<T, TProperty>(
            this IQuery<T> query,
            Expression<Func<T, TProperty>> selector
        ) => new SelectQuery<T, T, TProperty>(query, selector);

        public static IQuery<TBase, TProperty> Select<TBase, T, TProperty>(
            this IQuery<TBase, T> query,
            Expression<Func<T, TProperty>> selector
        ) => new SelectQuery<TBase, T, TProperty>(query, selector);
        #endregion

        #region SelectMany
        public static IQuery<T, TProperty> SelectMany<T, TProperty>(
            this IQuery<T> query,
            Expression<Func<T, IEnumerable<TProperty>>> selector
        ) => new SelectManyQuery<T, T, TProperty>(query, selector);

        public static IQuery<TBase, TProperty> SelectMany<TBase, T, TProperty>(
            this IQuery<TBase, T> query,
            Expression<Func<T, IEnumerable<TProperty>>> selector
        ) => new SelectManyQuery<TBase, T, TProperty>(query, selector);
        #endregion

        #region Skip
        public static IQuery<T> Skip<T>(
            this IQuery<T> query,
            uint amount
        ) => new SkipQuery<T>(query, amount);

        public static IQuery<TBase, T> Skip<TBase, T>(
            this IQuery<TBase, T> query,
            uint amount
        ) => new SkipQuery<TBase, T>(query, amount);
        #endregion

        #region Take
        public static IQuery<T> Take<T>(
            this IQuery<T> query,
            uint amount
        ) => new TakeQuery<T>(query, amount);

        public static IQuery<TBase, T> Take<TBase, T>(
            this IQuery<TBase, T> query,
            uint amount
        ) => new TakeQuery<TBase, T>(query, amount);
        #endregion

        #region Where
        public static IQuery<T> Where<T>(
            this IQuery<T> query,
            Expression<Func<T, bool>> criteria
        ) => new WhereQuery<T>(query, criteria);

        public static IQuery<TBase, T> Where<TBase, T>(
            this IQuery<TBase, T> query,
            Expression<Func<T, bool>> criteria
        ) => new WhereQuery<TBase, T>(query, criteria);
        #endregion
    }
}
