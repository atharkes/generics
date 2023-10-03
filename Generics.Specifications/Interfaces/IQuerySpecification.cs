namespace Generics.Specifications.Interfaces {
    /// <summary> A <see cref="IQuerySpecification{T}"/> to specify what you want to retrieve from a repository. </summary>
    /// <typeparam name="T">The <see cref="Type"/> the <see cref="ISpecification{T}"/> operates on.</typeparam>
    public interface IQuerySpecification<T> : ISpecification<T>, IQuerySpecification<T, T> {
        /// <summary> The <see cref="IQuery{T}"/> that stores what the <see cref="ISpecification{T}"/> is specifying. </summary>
        new IQuery<T> Query { get; }

        /// <summary> The <see cref="IQuery{TBase, TResult}"/> as specified by the <see cref="ISpecification{TBase, TResult}"/> interface. </summary>
        IQuery<T, T> IQuerySpecification<T, T>.Query => Query;
    }

    /// <summary> A <see cref="ISpecification{TBase, TResult}"/> to specify what you want to retrieve from a repository. </summary>
    /// <typeparam name="TBase">The <see cref="Type"/> the <see cref="ISpecification{TBase, TResult}"/> operates on.</typeparam>
    /// <typeparam name="TResult">The <see cref="Type"/> that should be returned as result.</typeparam>
    public interface IQuerySpecification<in TBase, out TResult> : ISpecification<TBase, TResult> {
        /// <summary> The <see cref="IQuery{TBase, TResult}"/> that stores what the <see cref="ISpecification{TBase, TResult}"/> is specifying. </summary>
        IQuery<TBase, TResult> Query { get; }

        /// <summary> Apply the <see cref="ISpecification{TBase, TResult}"/> to a <paramref name="queryable"/>. </summary>
        /// <param name="queryable">The <see cref="IQueryable{T}"/> to apply the <see cref="ISpecification{TBase, TResult}"/> to.</param>
        /// <returns>The <see cref="IQueryable{T}"/> after applying the <see cref="ISpecification{TBase, TResult}"/>.</returns>
        IQueryable<TResult> ISpecification<TBase, TResult>.Apply(IQueryable<TBase> queryable) => Query.Apply(queryable);
    }
}
