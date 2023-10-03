using Generics.Specifications.Interfaces;

namespace Generics.Specifications.Extensions {
    /// <summary> Extension methods for the <see cref="IQuerySpecification{T}"/> and <see cref="IQuerySpecification{TBase, TResult}"/> interfaces. </summary>
    public static class QuerySpecificationExtensions {
        /// <summary> Add an extra <paramref name="queryFunction"/> to a <paramref name="specification"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <param name="specification">The <see cref="IQuerySpecification{T}"/> to add a <paramref name="queryFunction"/> to.</param>
        /// <param name="queryFunction">The <see cref="Func{T, TResult}"/> to add to the <see cref="IQuerySpecification{TBase, TResult}"/>.</param>
        /// <returns>A new <see cref="IQuerySpecification{T}"/> that includes the new <paramref name="queryFunction"/>.</returns>
        public static IQuerySpecification<T> With<T>(
            this IQuerySpecification<T> specification,
            Func<IQuery<T>, IQuery<T>> queryFunction
        ) => new QuerySpecification<T>(queryFunction.Invoke(specification.Query));

        /// <summary> Add an extra <paramref name="queryFunction"/> to a <paramref name="specification"/>. </summary>
        /// <typeparam name="TBase">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> specified as result by the <paramref name="specification"/>.</typeparam>
        /// <typeparam name="TNewResult">The new <see cref="Type"/> specified as result by the created <see cref="IQuerySpecification{TBase, TResult}"/>.</typeparam>
        /// <param name="specification">The <see cref="IQuerySpecification{TBase, TResult}"/> to add a <paramref name="queryFunction"/> to.</param>
        /// <param name="queryFunction">The <see cref="Func{T, TResult}"/> to add to the <see cref="IQuerySpecification{TBase, TResult}"/>.</param>
        /// <returns>A new <see cref="IQuerySpecification{TBase, TResult}"/> that includes the new <paramref name="queryFunction"/>.</returns>
        public static IQuerySpecification<TBase, TNewResult> With<TBase, TResult, TNewResult>(
            this IQuerySpecification<TBase, TResult> specification,
            Func<IQuery<TBase, TResult>, IQuery<TBase, TNewResult>> queryFunction
        ) => new QuerySpecification<TBase, TNewResult>(queryFunction.Invoke(specification.Query));
    }
}
