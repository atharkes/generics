namespace Generics.Specifications.Interfaces {
    /// <summary> A <see cref="ISpecification{T}"/> to specify what you want to retrieve from a repository. </summary>
    /// <typeparam name="T">The <see cref="Type"/> the <see cref="ISpecification{T}"/> operates on.</typeparam>
    public interface ISpecification<T> : ISpecification<T, T> { }

    /// <summary> A <see cref="ISpecification{TBase, TResult}"/> to specify what you want to retrieve from a repository. </summary>
    /// <typeparam name="TBase">The <see cref="Type"/> the <see cref="ISpecification{TBase, TResult}"/> operates on.</typeparam>
    /// <typeparam name="TResult">The <see cref="Type"/> that should be returned as result.</typeparam>
    public interface ISpecification<in TBase, out TResult> {
        /// <summary> Apply the <see cref="ISpecification{TBase, TResult}"/> to a <paramref name="queryable"/>. </summary>
        /// <param name="queryable">The <see cref="IQueryable{T}"/> to apply the <see cref="ISpecification{TBase, TResult}"/> to.</param>
        /// <returns>The <see cref="IQueryable{T}"/> after applying the <see cref="ISpecification{TBase, TResult}"/>.</returns>
        IQueryable<TResult> Apply(IQueryable<TBase> queryable);
    }
}
