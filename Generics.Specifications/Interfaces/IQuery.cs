namespace Generics.Specifications.Interfaces {
    /// <summary> An <see cref="IQuery{T}"/> that stores a query. </summary>
    /// <typeparam name="T">The <see cref="Type"/> the <see cref="IQuery{T}"/> operates on.</typeparam>
    public interface IQuery<T> : IQuery<T, T> { }

    /// <summary> An <see cref="IQuery{TBase, TResult}"/> that stores a query. </summary>
    /// <typeparam name="TBase">The <see cref="Type"/> the <see cref="IQuery{T}"/> operates on.</typeparam>
    /// <typeparam name="TResult">The <see cref="Type"/> that should be returned as result.</typeparam>
    public interface IQuery<in TBase, out TResult> {
        /// <summary> Apply the <see cref="IQuery{TBase, TResult}"/> to a <paramref name="queryable"/>. </summary>
        /// <param name="queryable">The <see cref="IQueryable{T}"/> to apply the <see cref="IQuery{TBase, TResult}"/> to.</param>
        /// <returns>The <see cref="IQueryable{T}"/> after applying the <see cref="IQuery{TBase, TResult}"/>.</returns>
        IQueryable<TResult> Apply(IQueryable<TBase> queryable);
    }
}
