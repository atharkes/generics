namespace Generics.Specifications.Interfaces {
    public interface IOrderedQuery<T> : IQuery<T>, IOrderedQuery<T, T> {
        new IOrderedQueryable<T> Apply(IQueryable<T> queryable);
        IOrderedQueryable<T> IOrderedQuery<T, T>.Apply(IQueryable<T> queryable) => Apply(queryable);
    }

    public interface IOrderedQuery<TBase, T> : IQuery<TBase, T> {
        new IOrderedQueryable<T> Apply(IQueryable<TBase> queryable);
        IQueryable<T> IQuery<TBase, T>.Apply(IQueryable<TBase> queryable) => Apply(queryable);
    }
}
