using Generics.Specifications.Interfaces;

namespace Generics.Specifications.Extensions {
    /// <summary> Extension methods for the <see cref="ISpecification{T}"/> and <see cref="ISpecification{TBase, TResult}"/> interfaces. </summary>
    public static class SpecificationExtensions {
        /// <summary> Add an extra <paramref name="queryFunction"/> to a <paramref name="specification"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{T}"/> to add a <paramref name="queryFunction"/> to.</param>
        /// <param name="queryFunction">The <see cref="Func{T, TResult}"/> to add to the <see cref="ISpecification{TBase, TResult}"/>.</param>
        /// <returns>A new <see cref="ISpecification{T}"/> that includes the new <paramref name="queryFunction"/>.</returns>
        public static ISpecification<T> With<T>(
            this ISpecification<T> specification,
            Func<IQuery<T>, IQuery<T>> queryFunction
            ) => new Specification<T>(queryFunction.Invoke(specification.Query));

        /// <summary> Add an extra <paramref name="queryFunction"/> to a <paramref name="specification"/>. </summary>
        /// <typeparam name="TBase">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> specified as result by the <paramref name="specification"/>.</typeparam>
        /// <typeparam name="TNewResult">The new <see cref="Type"/> specified as result by the created <see cref="ISpecification{TBase, TResult}"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> to add a <paramref name="queryFunction"/> to.</param>
        /// <param name="queryFunction">The <see cref="Func{T, TResult}"/> to add to the <see cref="ISpecification{TBase, TResult}"/>.</param>
        /// <returns>A new <see cref="ISpecification{TBase, TResult}"/> that includes the new <paramref name="queryFunction"/>.</returns>
        public static ISpecification<TBase, TNewResult> With<TBase, TResult, TNewResult>(
            this ISpecification<TBase, TResult> specification,
            Func<IQuery<TBase, TResult>, IQuery<TBase, TNewResult>> queryFunction
            ) => new Specification<TBase, TNewResult>(queryFunction.Invoke(specification.Query));
    }
}
