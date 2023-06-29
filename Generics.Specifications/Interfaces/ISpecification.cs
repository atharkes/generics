namespace Generics.Specifications.Interfaces {
    /// <summary> A <see cref="ISpecification{T}"/> to specify what you want to retrieve from a repository. </summary>
    /// <typeparam name="T">The <see cref="Type"/> the <see cref="ISpecification{T}"/> operates on.</typeparam>
    public interface ISpecification<T> : ISpecification<T, T> {
        /// <summary> The <see cref="IQuery{T}"/> that stores what the <see cref="ISpecification{T}"/> is specifying. </summary>
        new IQuery<T> Query { get; }

        /// <summary> The <see cref="IQuery{TBase, T}"/> as specified by the <see cref="ISpecification{TBase, TResult}"/> interface. </summary>
        IQuery<T, T> ISpecification<T, T>.Query => Query;

        /// <summary> Add an extra <paramref name="queryFunction"/> to the <see cref="ISpecification{T}"/>. </summary>
        /// <param name="queryFunction">The <see cref="Func{T, TResult}"/> to add to the <see cref="ISpecification{T}"/>.</param>
        /// <returns>A new <see cref="ISpecification{T}"/> that includes the new <paramref name="queryFunction"/>.</returns>
        ISpecification<T> With(Func<IQuery<T>, IQuery<T>> queryFunction);
    }

    /// <summary> A <see cref="ISpecification{TBase, TResult}"/> to specify what you want to retrieve from a repository. </summary>
    /// <typeparam name="TBase">The <see cref="Type"/> the <see cref="ISpecification{TBase, TResult}"/> operates on.</typeparam>
    /// <typeparam name="TResult">The <see cref="Type"/> that should be returned as result.</typeparam>
    public interface ISpecification<TBase, TResult> {
        /// <summary> The <see cref="IQuery{T}"/> that stores what the <see cref="ISpecification{TBase, TResult}"/> is specifying. </summary>
        IQuery<TBase, TResult> Query { get; }

        /// <summary> Apply the <see cref="ISpecification{TBase, TResult}"/> to a <paramref name="queryable"/>. </summary>
        /// <param name="queryable">The <see cref="IQueryable{T}"/> to apply this <see cref="ISpecification{TBase, TResult}"/> to. </param>
        /// <returns>The <see cref="IQueryable{T}"/> after applying the <see cref="ISpecification{TBase, TResult}"/>.</returns>
        IQueryable<TResult> Apply(IQueryable<TBase> queryable) => Query.Apply(queryable);

        /// <summary> Add an extra <paramref name="queryFunction"/> to the <see cref="ISpecification{TBase, TResult}"/>. </summary>
        /// <typeparam name="TNewResult">The new <see cref="Type"/> of the result.</typeparam>
        /// <param name="queryFunction">The <see cref="Func{T, TResult}"/> to add to the <see cref="ISpecification{T}"/>.</param>
        /// <returns>A new <see cref="ISpecification{TBase, TResult}"/> that includes the new <paramref name="queryFunction"/>.</returns>
        ISpecification<TBase, TNewResult> With<TNewResult>(Func<IQuery<TBase, TResult>, IQuery<TBase, TNewResult>> queryFunction);
    }
}
