namespace Generics.Specifications.Interfaces {
    public interface IOrderedQuery<T> : IQuery<T>, IOrderedQuery<T, T> { }

    public interface IOrderedQuery<TBase, TResult> : IQuery<TBase, TResult> {
        new IOrderedQueryable<TResult> Apply(IQueryable<TBase> queryable);
    }
}
